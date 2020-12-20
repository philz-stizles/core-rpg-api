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

            // modelBuilder.Entity<Skill>()
            //     .HasData(new Skill{ });
        }
    }
}