using System.ComponentModel.DataAnnotations.Schema;

namespace core_rpg_mvc.Models
{
    [Table("Weapons")]
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
    }
}