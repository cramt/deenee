using System.Collections.Generic;

namespace Deenee.Model {
    public class CharacterSheet {
        public List<long> UsersOwned { get; set; } = new List<long>();
        public int Health { get; set; } = 0;

    }
}