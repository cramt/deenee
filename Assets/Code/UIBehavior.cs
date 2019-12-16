using PowerUI;
using UnityEngine;

namespace Deenee {
    public class UIBehavior : MainBehavior {
        public WorldUIHelper WorldUI;
        public HtmlDocument document;
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
        }
    }
}