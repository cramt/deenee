using SFB;
using System;
using System.IO;
using System.Net;
using System.Threading;
using UnityEngine;
using System.Drawing;
using Deenee.Model;
using System.Linq;

namespace Deenee {
    class Main {
#if UNITY_EDITOR
        public const bool IN_EDITOR = true;
#else
        public const bool IN_EDITOR = false;
#endif
        public static Thread UnityThread { get; private set; } = Thread.CurrentThread;
        public static string Get(string uri) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                return reader.ReadToEnd();
            }
        }
        public static System.Random Random { get; private set; } = new System.Random();
        public static DiscordBehavior DiscordBehavior { get; private set; }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Start() {
            Console.SetOut(new UnityLogWriter());
            //DiscordBehavior = new GameObject("discord handler1").AddComponent<DiscordBehavior>();
            //Campaign.New("hello");

            if (IN_EDITOR) {
                string assetsPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
                string webPath = Path.Combine(assetsPath, "Web");
                string newWebPath = Path.Combine(assetsPath, "Resources", "Web");
                if (Directory.Exists(webPath)) {
                    if (!Directory.Exists(newWebPath)) {
                        Directory.CreateDirectory(newWebPath);
                    }
                    Directory.GetFiles(webPath).ToList().ForEach(file => {
                        if (!file.EndsWith(".meta")) {
                            File.Copy(file, Path.Combine(newWebPath, new FileInfo(file).Name + ".bytes"), true);
                        }
                    });
                }
            }

        }
        public static Campaign campaign;
    }
}