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
            Campaign.New(@"C:\Users\1080622\Documents\code\deenee\campaigns", "test");
        }
    }
}