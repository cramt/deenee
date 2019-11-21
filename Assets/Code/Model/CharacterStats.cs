using System.Collections.Generic;

namespace Deenee.Model {
    public class CharacterStats {
        public CharacterSheet CharacterSheet { get; set; }
        public List<Condition> Conditions { get; set; } = new List<Condition>();
        public Token Token { get; set; }
        public int Health { get; set; } = 0;


    }
}