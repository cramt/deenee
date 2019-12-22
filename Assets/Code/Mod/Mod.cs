using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Deenee.Mod {

    public class Mod {
#if UNITY_EDITOR
        static string modsFolder { get; } = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "deenee_mods");
#else
        static string modsFolder { get; } = Path.Combine(Directory.GetCurrentDirectory(), "deenee_mods");
#endif
        public static List<Mod> Mods { get; private set; } = new List<Mod>();

        public static Task<Mod> AddNewMod(string gitPath) {
            return new Task<Mod>(() => {
                if (!Directory.Exists(modsFolder)) {
                    Directory.CreateDirectory(modsFolder);
                }
                if (gitPath.Substring(gitPath.Length - 4) != ".git") {
                    gitPath += ".git";
                }
                Uri url = new Uri(gitPath);
                string[] path = new List<string>() { url.Host }.Concat(url.AbsolutePath.Substring(1, url.AbsolutePath.Length - 5).Split('/').ToList()).ToArray();
                string modFolder = Path.Combine(modsFolder, string.Join("\\", path));
                if (!Directory.Exists(modFolder)) {
                    Directory.CreateDirectory(modFolder);
                    Repository.Clone(gitPath, modFolder);
                }
                else {
                    string logMessage = "";
                    using (var repo = new Repository(modFolder)) {
                        var remote = repo.Network.Remotes["origin"];
                        var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                        Commands.Fetch(repo, remote.Name, refSpecs, null, logMessage);
                    }
                }
                string packageJsonPath = Path.Combine(modFolder, "package.json");
                if (!File.Exists(packageJsonPath)) {
                    throw new ArgumentException("git rep does not include a package.json file");
                }
                HandleNPM(modFolder);
                return new Mod(packageJsonPath);
            });
        }

        public static Task<List<Mod>> Load(string path = null) {
            return Task<List<Mod>>.Factory.StartNew(() => {
                if (path == null) {
                    path = modsFolder;
                }
                string packagePath = Path.Combine(path, "package.json");
                if (File.Exists(packagePath)) {
                    HandleNPM(path);
                    new Mod(packagePath);
                    return null;
                }
                Task.WaitAll(Directory.GetDirectories(path).Select(x => Load(x)).ToArray());
                return Mods;
            });
        }

        private static void HandleNPM(string path) {
            Process npm = Process.Start(new ProcessStartInfo() {
                FileName = @"D:\programs\nodejs\npm.cmd",
                Arguments = "install --production --json",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                WorkingDirectory = path
            });
            string jsonString = "";
            bool jsonOutput = false;
            npm.OutputDataReceived += (object sender, DataReceivedEventArgs e) => {
                if (e.Data == "{") {
                    jsonOutput = true;
                }
                if (jsonOutput) {
                    jsonString += e.Data;
                }
                else {

                }
            };
            npm.BeginOutputReadLine();
            npm.WaitForExit();
        }

        public static void RunAll() {
            Mods.ForEach(x => x.Run());
        }


        private PackageJson package;
        private string mainJsFilePath;
        private string path;
        private string dirname;
        public string Name { get; private set; }

        private Mod(string packageJsonPath) {
            PackageJson package = JsonUtility.FromJson<PackageJson>(File.ReadAllText(packageJsonPath));
            this.package = package;
            dirname = new DirectoryInfo(packageJsonPath).Parent.FullName;
            mainJsFilePath = Path.Combine(dirname, package.main);
            Name = package.name;
            path = packageJsonPath;
            Mods.Add(this);
        }

        public void Run() {
            
        }

        private void evaluate() {

        }
    }
}