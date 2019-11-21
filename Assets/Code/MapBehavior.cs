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
                ExtensionFilter objFilter = new ExtensionFilter("3D files", "3D", "3DS", "3MF", "AC", "AC3D", "ACC", "AMF", "AMJ", "ASE", "ASK", "B3D", "BLEND", "BVH", "COB", "DAE", "DXF", "ENFF", "FBX", "IRR", "LWO", "LWS", "LXO", "MD2", "MD3", "MD5", "MDC", "MDL", "MESH", "MOT", "MS3D", "NDO", "NFF", "OBJ", "OFF", "OGEX", "PLY", "PMX", "PRJ", "Q3O", "Q3S", "RAW", "SCN", "SIB", "SMD", "STL", "TER", "UC", "VTA", "X", "X3D", "XGL", "ZGL");
                ExtensionFilter imageFilter = new ExtensionFilter("Image files", "png", "jpg", "jpeg");
                StandaloneFileBrowser.OpenFilePanelAsync("Load File", "", new ExtensionFilter[] {objFilter, imageFilter }, true, (string[] paths) => {
                    paths.ToList().ForEach(path => {
                        string name = new FileInfo(path).Name;
                        string[] split = name.Split('.');
                        string nameWOext = split[0];
                        string ext = split[1];
                        if (objFilter.Extensions.Contains(ext)) {
                            byte[] data = File.ReadAllBytes(path);
                            Main.campaign.AddObject(name, data);
                            InvokeFunction(() => {
                                using (var assetLoader = new AssetLoader()) {
                                    var assetLoaderOptions = AssetLoaderOptions.CreateInstance();
                                    var myGameObject = assetLoader.LoadFromMemory(data, name, assetLoaderOptions, cachedObjectHandler);
                                    UpdateCache();
                                }
                            });
                        }
                        else {

                        }
                    });
                });
            }
        }
    }
}