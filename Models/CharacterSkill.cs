using System.ComponentModel.DataAnnotations.Schema;

namespace core_rpg_mvc.Models
{
    // Many to Many
    [Table("CharacterSkills")]
    public class CharacterSkill
    {
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; }
        public int SkillId { get; set; }
        public virtual Skill Skill { get; set; }
    }
}