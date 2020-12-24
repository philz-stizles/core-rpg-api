using System.Collections.Generic;
using core_rpg_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace core_rpg_mvc.Data
{
    public class DataContext: DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CharacterSkill> CharacterSkills { get; set; }
        public DataContext(DbContextOptions<DataContext> options): base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(c => c.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(c => c.Role)
                .HasDefaultValue("Player");

            modelBuilder.Entity<Character>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<CharacterSkill>()
                .HasKey(c => new { c.CharacterId, c.SkillId });

            modelBuilder.Entity<Skill>()
                .HasData(
                    new Skill{ Id = 1, Name = "Fireball", Damage = 30 },
                    new Skill{ Id = 2, Name = "Frenzy", Damage = 20 },
                    new Skill{ Id = 3, Name = "Blizzard", Damage = 50 }
                );

            modelBuilder.Entity<Weapon>()
                .HasData(
                    new Weapon{ Id = 1, Name = "The Master Sword", Damage = 20 },
                    new Weapon{ Id = 2, Name = "Crystal Wand", Damage = 5 }
                );

            Utility.CreatePasswordHash("P@ssw0rd", out byte[] passwordHash, out byte[] passwordSalt);
            modelBuilder.Entity<User>()
                .HasData(new User{ Id = 1, Username = "Default", Email = "defaultUser@somemai.com", PasswordHash = passwordHash, PasswordSalt = passwordSalt });

            modelBuilder.Entity<Character>()
                .HasData(new Character{ Id = 1, Name = "Frodo", HitPoints = 100, Strength = 10, Defense = 10, 
                    Intelligence = 10, UserId = 1, WeaponId = 2 });

            modelBuilder.Entity<CharacterSkill>()
                .HasData(new CharacterSkill{ CharacterId = 1, SkillId = 2 });
        }
    }
}