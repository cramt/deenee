using System;
using System.Collections.Generic;

namespace Deenee {
    public class MenuEntry {
        public Dictionary<string, MenuEntry> SubEntires { get; set; } = null;
        public Action Function { get; set; } = null;
    }
}