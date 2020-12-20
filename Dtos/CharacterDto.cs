using System.Collections.Generic;
using core_rpg_mvc.Models;

namespace core_rpg_mvc.Dtos
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public CharacterType CharacterType { get; set; }
        public string Weapon { get; set; }
        public ICollection<string> Skills { get; set; }
    }
}