using Deenee.Model;

namespace Deenee {
    public class TokenBehavior : MainBehavior, IMenuOpenable {
        public Token Token { get; set; }

        public MenuEntry Menu {
            get {
                return new MenuEntry();
            }
        }

        public override void OnStart(OnStartProperties onStartProperties) {
            base.OnStart(onStartProperties);
        }

        public override void OnUpdate(OnUpdateProperties onUpdateProperties) {
            base.OnUpdate(onUpdateProperties);
        }
    }
}