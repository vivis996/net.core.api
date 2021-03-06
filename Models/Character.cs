using System.Collections.Generic;

namespace net.core.api.Models
{
  public class Character
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int HitPoints { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Intelligence { get; set; }
    public RpgClass Class { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public Weapon Weapon { get; set; }
    public List<Skill> Skills { get; set; }
    public int Fights { get; set; }
    public int Victories { get; set; }
    public int Defeats { get; set; }

    public Character()
    {
      this.SetDefault();
    }

    public void SetDefault()
    {
      this.Name = "Obi Wan";
      this.HitPoints = 100;
      this.Strength = 10;
      this.Defense = 10;
      this.Intelligence = 10;
      this.Class = RpgClass.Jedi;
    }
  }
}