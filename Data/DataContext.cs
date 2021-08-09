using Microsoft.EntityFrameworkCore;
using net.core.api.Models;

namespace net.core.api.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Character> Characters { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Weapon> Weapons { get; set; }
    public DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Skill>().HasData(
        new Skill { Id = 1, Name = "Force", Damage = 100, },
        new Skill { Id = 2, Name = "Dark Rays", Damage = 110, },
        new Skill { Id = 3, Name = "Push", Damage = 80, }
      );

      modelBuilder.Entity<User>()
          .Property(user => user.Role).HasDefaultValue("Player");

      const string password = "123456";
      Utility.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

      modelBuilder.Entity<User>().HasData(
        new User { Id = 1, Username = "User1", PasswordHash = passwordHash, PasswordSalt = passwordSalt, },
        new User { Id = 2, Username = "User2", PasswordHash = passwordHash, PasswordSalt = passwordSalt, }
      );

      modelBuilder.Entity<Character>().HasData(
        new Character
        {
          Id = 1,
          Name = "Obi wan",
          HitPoints = 1250,
          Strength = 90,
          Defense = 120,
          Intelligence = 125,
          Class = RpgClass.Jedi,
          UserId = 1,
        },
        new Character
        {
          Id = 2,
          Name = "Darth Vader",
          HitPoints = 1100,
          Strength = 110,
          Defense = 100,
          Intelligence = 112,
          Class = RpgClass.Sith,
          UserId = 2,
        }
      );

      modelBuilder.Entity<Weapon>().HasData(
        new Weapon { Id = 1, Name = "Dark Saber", Damage = 105, CharacterId = 1, },
        new Weapon { Id = 2, Name = "Red lightsaber", Damage = 100, CharacterId = 2, }
      );

      /*
        Look at "144. Seeding Entities with Relations" because it has other modelBuilder
        for characterskills table (he added another class).

        modelBuilder.Entity<CharacterSkill>().HasData(
          new CharacterSkill { CharacterId = 1, SkillId = 1 },
          new CharacterSkill { CharacterId = 1, SkillId = 3 },
          new CharacterSkill { CharacterId = 2, SkillId = 1 },
          new CharacterSkill { CharacterId = 2, SkillId = 2 }
        );

        So you need need to add these manually.
      */
    }
  }
}