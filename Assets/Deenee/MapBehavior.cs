using Deenee.Model;
using SFB;
using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Deenee {
    public class MapBehavior : MainBehavior {
        public Map Map { get; set; }

        private void UpdateCache() {
            foreach(Renderer r in Map.Campaign.CachedObjectHandler.GetComponentsInChildren<Renderer>()) {
                r.enabled = false;
            }
        }
        public override void OnStart(OnStartProperties onStartProperties) {
            base.OnStart(onStartProperties);
            /*
            if (!File.Exists(@"C:\Users\1080622\Documents\code\deenee\campaigns\test.campaign")) {
                Main.campaign = Campaign.New(@"C:\Users\1080622\Documents\code\deenee\campaigns", "test");
            }
            else {
                Main.campaign = Campaign.Load(@"C:\Users\1080622\Documents\code\deenee\campaigns\test.campaign");
            }
            Map map = new Map();
            map.Name = "idk";
            map.Campaign = Main.campaign;
            Main.campaign.Maps.Add(map);
            Console.WriteLine(Main.campaign.Maps.Count);
            Map = Main.campaign.Maps[0];
            */
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
                        if (objFilter.Extensions.Contains(ext.ToUpper())) {
                            byte[] data = File.ReadAllBytes(path);
                            Main.campaign.AddObject(name, data);                   
                            //TODO: actually do this
                        }
                        else {

                        }
                    });
                });
            }
        }
    }
}