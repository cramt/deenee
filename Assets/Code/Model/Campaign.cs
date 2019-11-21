using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Deenee.Model {
    [Serializable]
    public class Campaign {
        private string path = "";
        public string Name { get; private set; } = "";
        public Dictionary<string, byte[]> Objects { get; private set; } = new Dictionary<string, byte[]>();
        public List<Map> Maps { get; set; } = new List<Map>();
        public static Campaign Load(string path) {
            Campaign res = new Campaign {
                Name = new FileInfo(path).Name.Split('.')[0],
                path = path
            };
            using (var file = File.OpenRead(path))
            using (var zip = new ZipArchive(file, ZipArchiveMode.Read)) {
                zip.Entries.ToList().ForEach(x => {
                    if (x.Name == "") {
                        return;
                    }
                    if(x.FullName == "maps") {
                        BinaryFormatter formatter = new BinaryFormatter();
                        res.Maps = (List<Map>)formatter.Deserialize(x.Open());
                        return;
                    }
                    List<string> split = x.FullName.Split('/').ToList();
                    if (split[0] == "objects") {
                        split.RemoveAt(0);
                        MemoryStream memory = new MemoryStream();
                        x.Open().CopyTo(memory);
                        res.Objects.Add(string.Join("/", split.ToArray()), memory.ToArray());
                    }
                });
            }
            return res;
        }
        public void Save() {
            using (var memoryStream = new MemoryStream()) {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true)) {
                    archive.CreateEntry("objects/");
                    var mapsFile = archive.CreateEntry("maps");
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(mapsFile.Open(), Maps);
                    Objects.ToList().ForEach(x => {
                        var entry = archive.CreateEntry("objects/" + x.Key);
                        new MemoryStream(x.Value).CopyTo(entry.Open());
                    });
                }
                using (var fileStream = new FileStream(Path.Combine(path), FileMode.Create)) {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.CopyTo(fileStream);
                }
            }
        }
        public static Campaign New(string path, string name) {
            Campaign res = new Campaign();
            res.path = Path.Combine(path, name + ".campaign");
            res.Name = name;
            res.Save();
            return res;
        }
    }
}