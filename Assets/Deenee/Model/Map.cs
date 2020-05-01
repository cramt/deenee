using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

namespace Deenee.Model {
    [Serializable]
    public class Map {
        public Campaign Campaign { get; set; }
        public List<Token> Tokens { get; set; } = new List<Token>();
        public string Name { get; set; }
        public void Create() {
            Tokens.ForEach(x => {
                x.Create();
            });

        }
    }
}