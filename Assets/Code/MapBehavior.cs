using Deenee.Model;
using Dummiesman;
using SFB;
using System;
using System.IO;
using System.Linq;
using System.Text;
using TriLib;
using UnityEngine;

namespace Deenee {
    public class MapBehavior : MainBehavior {
        public Map Map { get; set; }

        private GameObject cachedObjectHandler;
        private void UpdateCache() {
            foreach(Renderer r in cachedObjectHandler.GetComponentsInChildren<Renderer>()) {
                r.enabled = false;
            }
        }
        public override void OnStart(OnStartProperties onStartProperties) {
            base.OnStart(onStartProperties);
            cachedObjectHandler = new GameObject("cachedObjectHandler");
            
        }

        public override void OnUpdate(OnUpdateProperties onUpdateProperties) {
            base.OnUpdate(onUpdateProperties);

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L)) {
                StandaloneFileBrowser.OpenFilePanelAsync("Load File", "", new[] {
                    new ExtensionFilter("3D files", "obj"),
                    new ExtensionFilter("Image files", "png", "jpg", "jpeg")
                }, true, (string[] paths) => {
                    paths.ToList().ForEach(path => {
                        Console.WriteLine(path);
                        string name = new FileInfo(path).Name;
                        string[] split = name.Split('.');
                        string nameWOext = split[0];
                        string ext = split[1];
                        if (ext == "obj") {
                            byte[] data = File.ReadAllBytes(path);
                            Main.campaign.AddObject(name, data);
                            InvokeFunction(() => {
                                using (var assetLoader = new AssetLoader()) {
                                    var assetLoaderOptions = AssetLoaderOptions.CreateInstance();   //Creates the AssetLoaderOptions instance.
                                                                                                    //AssetLoaderOptions let you specify options to load your model.
                                                                                                    //(Optional) You can skip this object creation and it's parameter or pass null.

                                    //You can modify assetLoaderOptions before passing it to LoadFromFile method. You can check the AssetLoaderOptions API reference at:
                                    //https://ricardoreis.net/trilib/manual/html/class_tri_lib_1_1_asset_loader_options.html
                                    //(Optional) You can skip this object creation and it's parameter or pass null.
                                    UpdateCache();
                                    var myGameObject = assetLoader.LoadFromMemory(data, name, assetLoaderOptions, cachedObjectHandler);
                                }
                            });

                            /*
                            var obj = RawObject.Create(File.ReadAllText(path));
                            string name = nameWOext;
                            int index = 1;
                            while (Map.Campaign.RawObjects.FirstOrDefault(x => x.Name == name) != null) {
                                name = nameWOext + "~" + index++;
                            }
                            int id = Map.Campaign.RawObjects.Count;
                            Map.Campaign.RawObjects.Add(obj);
                            Token a = new Token();
                            a.RawObjectId = id;
                            a.Map = Map;
                            a.Create();
                            */
                        }
                        else {

                        }
                    });
                });
            }
        }
    }
}