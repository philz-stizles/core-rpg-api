using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core_rpg_mvc.Models
{
    [Table("Characters")]
    public class Character
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        [Required]
        public CharacterType CharacterType { get; set; } = CharacterType.Knight;
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int? WeaponId { get; set; }
        public virtual Weapon Weapon { get; set; }
        public int Fights { get; set; } = 100;
        public int Victories { get; set; } = 10;
        public int Defeats { get; set; } = 10;
        public virtual ICollection<CharacterSkill> Skills { get; set; }
    }
}