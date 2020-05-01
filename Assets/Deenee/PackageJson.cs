using System;

namespace Deenee {
    //https://gist.github.com/iainreid820/5c1cc527fe6b5b7dba41fec7fe54bf6e
    [Serializable]
    public class PackageJson {
        [Serializable]
        public class Directories {
            public string bin;
            public string lib;
            public string man;
            public string doc;
            public string example;
        }
        [Serializable]
        public class Config {
            public string name;
            public object config;
        }
        [Serializable]
        public class Engine {
            public string node;
            public string npm;
        }
        [Serializable]
        public class PublishConfig {
            public string registry;
        }
        public string name;
        public string version;
        public string description;
        public string[] keywords;
        public string homepage;
        public dynamic bugs;
        public string license;
        public dynamic author;
        public dynamic contributors;
        public string[] files;
        public string main;
        public dynamic bin;
        public dynamic man;
        public Directories directories;
        public dynamic repository;
        public dynamic scripts;
        public Config config;
        public dynamic dependencies;
        public dynamic devDependencies;
        public dynamic peerDependencies;
        public dynamic optionalDependencies;
        public string[] bundledDependencies;
        public Engine engine;
        public string[] os;
        public string[] cpu;
        public bool preferGlobal;
        public bool @private;
        public PublishConfig publishConfig;
    }
}