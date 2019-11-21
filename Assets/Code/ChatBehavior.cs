using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Deenee {
    [RequireComponent(typeof(Canvas))]
    public class ChatBehavior : MainBehavior {
        private IChatInteractable[] interactives = new IChatInteractable[] {
            new ChatDice()
        };
        public InputField ChatInput;
        public override void OnStart(OnStartProperties onStartProperties) {
            base.OnStart(onStartProperties);

            ChatInput.onEndEdit.AddListener(new UnityEngine.Events.UnityAction<string>(str => {
                if (str.StartsWith("/")) {
                    str += str.Substring(1);
                }
                else {
                    return;
                }
                var args = str.splitNotBetween(' ', '"', '"').ToList();
                var func = args[0];
                args.RemoveAt(0);
                var inter = interactives.Where(x => x.Comamnds.Contains(func));
                inter.ToList().ForEach(x => {

                });
            }));
        }

        public override void OnUpdate(OnUpdateProperties onUpdateProperties) {
            base.OnUpdate(onUpdateProperties);
        }
    }
}