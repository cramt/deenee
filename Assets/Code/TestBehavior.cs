using System;
using System.Collections.Generic;

namespace Deenee {
    public class MenuEntry {
        public Dictionary<string, MenuEntry> SubEntires { get; set; } = null;
        public Action Function { get; set; } = null;
    }
    public interface IMenuOpenable {

        MenuEntry Menu { get; }
    }
    public class TestBehavior : MainBehavior, IMenuOpenable {

        public MenuEntry Menu { get; set; } = new MenuEntry {
            SubEntires = new Dictionary<string, MenuEntry> {
                { "idk", new MenuEntry {
                    Function = new Action(() => {
                        Console.WriteLine("hello there");
                    })
                }},
                { "something else", new MenuEntry {
                    Function = new Action(() => {
                        Console.WriteLine("dude");
                    })
                }},
                {
                    "yo", new MenuEntry {
                        SubEntires = new Dictionary<string, MenuEntry> {
                            {"yoin", new MenuEntry {
                                Function = new Action(() => {
                                    Console.WriteLine("yo");
                                })
                            } }
                        }
                    }
                }
            }
        };

        public override void OnApplicationQuit(OnApplicationQuitProperties onUpdateProperties) {
            base.OnApplicationQuit(onUpdateProperties);
        }

        public override void OnStart(OnStartProperties onStartProperties) {
            base.OnStart(onStartProperties);
        }

        public override void OnUpdate(OnUpdateProperties onUpdateProperties) {
            base.OnUpdate(onUpdateProperties);
        }
    }
}