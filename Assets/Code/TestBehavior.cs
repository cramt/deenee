using System;   
using System.Collections.Generic;

namespace Deenee {
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
                                }),
                                SubEntires = new Dictionary<string, MenuEntry> {
                                    {"bruh", new MenuEntry {

                                    } }
                                }
                            } }
                        }
                    }
                }
            }
        };

        public override void OnAppQuit(OnApplicationQuitProperties onUpdateProperties) {
            base.OnAppQuit(onUpdateProperties);
        }

        public override void OnStart(OnStartProperties onStartProperties) {
            base.OnStart(onStartProperties);
        }

        public override void OnUpdate(OnUpdateProperties onUpdateProperties) {
            base.OnUpdate(onUpdateProperties);
        }
    }
}