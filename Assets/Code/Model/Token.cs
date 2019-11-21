﻿using System;
using UnityEngine;

namespace Deenee.Model {
    [Serializable]
    public class Token {
        public string ObjectName { get; set; }
        public Map Map { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public GameObject Create() {
            GameObject go = new GameObject();
            //TODO
            return go;
        }
    }
}