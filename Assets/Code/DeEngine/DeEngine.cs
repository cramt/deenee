using Jint;
using Jint.Runtime.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Deenee.DeEngine {
    public class DeEngine {
        public Engine JintEngine { get; private set; }
        public DeEngine(PackageJson package, string path, string filename = null) {
            JintEngine = new Engine(cfg => cfg.AllowClr(typeof(Component).Assembly));
            string packageDir = new DirectoryInfo(path).Parent.FullName;
            if (filename == null) {
                filename = Path.Combine(packageDir, package.main);
            }
            if (!File.Exists(filename)) {
                if (File.Exists(filename + ".js")) {
                    filename += ".js";
                }
                else if (File.Exists(filename + "\\index.js")) {
                    filename += "\\index.js";
                }
            }
            string dirname = new DirectoryInfo(filename).Parent.FullName;
            JintEngine.SetValue("__filename", filename);
            JintEngine.SetValue("__dirname", packageDir);
            JintEngine.SetValue("hooks", DeEngineBindings.Hooks.Instance);
            JintEngine.SetValue("console", DeEngineBindings.Console.Instance);
            JintEngine.SetValue("ui", new {
                Canvas = TypeReference.CreateTypeReference(JintEngine, typeof(DeEngineBindings.UICanvas)),
                Text = TypeReference.CreateTypeReference(JintEngine, typeof(DeEngineBindings.UIText)),
                createComponent = DeEngineBindings.createComponent
            }) ;
            JintEngine.SetValue("Color", TypeReference.CreateTypeReference(JintEngine, typeof(Color)));
            JintEngine.SetValue("Vector3", TypeReference.CreateTypeReference(JintEngine, typeof(Vector3)));
            JintEngine.SetValue("require", new Func<string, object>((string str) => {
                try {
                    if (str.StartsWith(".")) {
                        str = str.Replace('/', '\\');
                        string file = Path.Combine(dirname, str);
                        string dir = new DirectoryInfo(file).Parent.FullName;
                        DeEngine newEngine = new DeEngine(package, path, file);
                        return newEngine.JintEngine.GetValue("exports");
                    }
                    else {
                        string nodeModules = "";
                        if (packageDir.Contains("node_modules")) {
                            DirectoryInfo currentDir = new DirectoryInfo(packageDir);
                            while (currentDir.Name != "node_modules") {
                                currentDir = currentDir.Parent;
                            }
                            nodeModules = currentDir.FullName;
                        }
                        else {
                            nodeModules = Path.Combine(packageDir, "node_modules");
                        }
                        if (Directory.Exists(nodeModules)) {
                            KeyValuePair<string, PackageJson> thisPackage = Directory.GetDirectories(nodeModules).ToList()
                            .Select(x => {
                                string thisPackagePath = Path.Combine(nodeModules, x, "package.json");
                                return new KeyValuePair<string, PackageJson>(thisPackagePath, JsonUtility.FromJson<PackageJson>(thisPackagePath));
                            })
                            .FirstOrDefault(x => x.Value.name == str);

                            if (!thisPackage.Equals(default(KeyValuePair<string, PackageJson>))) {
                                DeEngine newEngine = new DeEngine(thisPackage.Value, thisPackage.Key);
                                return newEngine.JintEngine.GetValue("exports");
                            }
                        }
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
                return null;
            }));
            try {
                JintEngine.Execute("\"use strict\";var exports={};(function(){" + File.ReadAllText(filename) + "})();");
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}