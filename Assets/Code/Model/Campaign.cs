using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Deenee.Model {
    [Serializable]
    public class Campaign : IDisposable {
        private string path = "";

        public string Name { get; private set; } = "";
        private Dictionary<string, byte[]> loadedObjects = new Dictionary<string, byte[]>();
        private Dictionary<string, ZipArchiveEntry> unloadedObjects = new Dictionary<string, ZipArchiveEntry>();
        public GameObject CachedObjectHandler { get; private set; }
        private ZipArchive zip;
        private FileStream file;
        public List<Map> Maps { get; set; } = new List<Map>();
        public byte[] GetObject(string path) {
            if (loadedObjects.ContainsKey(path)) {
                return loadedObjects[path];
            }
            if (unloadedObjects.ContainsKey(path)) {
                MemoryStream memory = new MemoryStream();
                unloadedObjects[path].Open().CopyTo(memory);
                byte[] data = memory.ToArray();
                loadedObjects.Add(path, data);
                return data;
            }
            return null;
        }
        public void AddObject(string name, byte[] data) {
            unloadedObjects.Add(name, null);
            loadedObjects.Add(name, data);
            //TODO add to zip file
        }
        private Campaign() {

        }
        public static Campaign Load(string path) {
            Campaign res = new Campaign {
                Name = new FileInfo(path).Name.Split('.')[0],
                path = path
            };
            res.CachedObjectHandler = new GameObject("cachedObjectHandler");
            UnityEngine.Object.DontDestroyOnLoad(res.CachedObjectHandler);
            res.file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            res.zip = new ZipArchive(res.file, ZipArchiveMode.Update);
            res.zip.Entries.ToList().ForEach(x => {
                if (x.Name == "") {
                    return;
                }
                if (x.FullName == "maps") {
                    BinaryFormatter formatter = new BinaryFormatter();
                    res.Maps = ((Map[])formatter.Deserialize(x.Open())).ToList();
                    return;
                }
                List<string> split = x.FullName.Split('/').ToList();
                if (split[0] == "objects") {
                    split.RemoveAt(0);
                    res.unloadedObjects.Add(string.Join("/", split.ToArray()), x);
                }
            });

            return res;
        }
        public void Save() {
            //TOOD, make work
            Maps.ForEach(x => x.Campaign = null);
            zip.GetEntry("objects/");
            var mapsFile = zip.GetEntry("maps");
            BinaryFormatter formatter = new BinaryFormatter();
            Console.WriteLine(Maps.Count);
            formatter.Serialize(mapsFile.Open(), Maps.ToArray());
            unloadedObjects.ToList().ForEach(x => {
                string path = "objects/" + x.Key;
                if (zip.Entries.Any(x => x.FullName == path)) {
                    return;
                }
                var entry = zip.CreateEntry(path);
                new MemoryStream(GetObject(x.Key)).CopyTo(entry.Open());
            });
            Maps.ForEach(x => x.Campaign = this);
        }
        public static Campaign New(string path, string name) {
            path = Path.Combine(path, name + ".campaign");
            using (var memoryStream = new MemoryStream()) {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true)) {
                    archive.CreateEntry("objects/");
                    var mapsFile = archive.CreateEntry("maps");
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(mapsFile.Open(), new Map[] { });
                }
                using (var fileStream = new FileStream(path, FileMode.Create)) {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.CopyTo(fileStream);
                }
            }
            return Load(path);
        }

        public void Dispose() {
            file.Close();
        }
    }
}