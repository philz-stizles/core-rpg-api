using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace core_rpg_mvc.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual ICollection<Character> Characters { get; set; } // One to Many r/ship
    }
}