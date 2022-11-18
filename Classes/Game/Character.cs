using SKA_Novel.Classes.Technical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKA_Novel.Classes.Game
{
    internal class Character
    {
        public string CharacterName { get; }

        public Character(string characterName)
        {
            CharacterName = characterName;
        }
    }
}
