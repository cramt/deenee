using PowerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deenee {
    public class UIBehavior : MainBehavior {
        public WorldUIHelper WorldUI;
        public HtmlDocument document;
        public Dictionary<string, object> arguments = new Dictionary<string, object>();
        protected string htmlFile = null;
        public override void OnStart(OnStartProperties onStartProperties) {
            base.OnStart(onStartProperties);
            GameObject resource = Resources.Load<GameObject>("BasicPowerUI");
            if (htmlFile != null) {
                resource.GetComponent<WorldUIHelper>().HtmlFile = Resources.Load<TextAsset>(htmlFile);
            }
            WorldUI = Instantiate(resource).GetComponent<WorldUIHelper>();
            WorldUI.transform.eulerAngles = Vector3.zero;
            document = WorldUI.document;
            WorldUI.transform.SetParent(this.transform);

            arguments.ToList().ForEach(x => {
                document.JavascriptEngine.Engine.SetValue(x.Key, x.Value);
            });
        }
    }
}