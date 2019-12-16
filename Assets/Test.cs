using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deenee {
    public class Test : UIBehavior {
        public Test() {
            htmlFile = "MyNewHtml";
        }

        public override void OnStart(OnStartProperties onStartProperties) {
            base.OnStart(onStartProperties);
            
        }
        public override void OnUpdate(OnUpdateProperties onUpdateProperties) {
            base.OnUpdate(onUpdateProperties);
            //Console.WriteLine(document.getElementByTagName("p").innerText);
        }
    }
}
