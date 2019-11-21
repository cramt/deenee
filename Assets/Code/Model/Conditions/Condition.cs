namespace Deenee.Model {
    public abstract class Condition {
        //TODO, implement all of these https://roll20.net/compendium/dnd5e/Conditions#content
        public abstract string Name { get; }
        public Token Concentration { get; set; }
    }
}