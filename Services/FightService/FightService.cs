using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using net.core.api.Data;
using net.core.api.Dtos.Fight;
using net.core.api.Models;

namespace net.core.api.Services.FightService
{
  public class FightService : IFightService
  {
    private readonly DataContext _context;
    public FightService(DataContext context)
    {
      this._context = context;
    }

    public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
    {
      var response = new ServiceResponse<AttackResultDto>();
      try
      {
        var attacker = await this._context.Characters
                .Include(c => c.Weapon)
                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
        var opponent = await this._context.Characters
                .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

        var damage = DoWeaponAttack(attacker, opponent);

        if (opponent.HitPoints <= 0)
        {
          response.Message = $"{opponent.Name} has been defeated!";
        }

        await this._context.SaveChangesAsync();
        response.Data = new AttackResultDto
        {
          Attacker = attacker.Name,
          AttackerHP = attacker.HitPoints,
          Opponent = opponent.Name,
          OpponentHP = opponent.HitPoints,
          Damage = damage,
        };
      }
      catch (Exception ex)
      {
        response.Success = false;
        response.Message = ex.Message;
      }

      return response;
    }

    private static int DoWeaponAttack(Character attacker, Character opponent)
    {
      var damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
      damage -= new Random().Next(opponent.Defense);

      if (damage > 0)
      {
        opponent.HitPoints -= damage;
      }

      return damage;
    }

    public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
    {
      var response = new ServiceResponse<AttackResultDto>();
      try
      {
        var attacker = await this._context.Characters
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
        var opponent = await this._context.Characters
                .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

        var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
        if (skill == null)
        {
          throw new Exception($"{attacker.Name} doesn't know this skill.");
        }

        var damage = DoSkillAttack(attacker, opponent, skill);

        if (opponent.HitPoints <= 0)
        {
          response.Message = $"{opponent.Name} has been defeated!";
        }

        await this._context.SaveChangesAsync();
        response.Data = new AttackResultDto
        {
          Attacker = attacker.Name,
          AttackerHP = attacker.HitPoints,
          Opponent = opponent.Name,
          OpponentHP = opponent.HitPoints,
          Damage = damage,
        };
      }
      catch (Exception ex)
      {
        response.Success = false;
        response.Message = ex.Message;
      }

      return response;
    }

    private static int DoSkillAttack(Character attacker, Character opponent, Skill skill)
    {
      var damage = skill.Damage + (new Random().Next(attacker.Intelligence));
      damage -= new Random().Next(opponent.Defense);

      if (damage > 0)
      {
        opponent.HitPoints -= damage;
      }

      return damage;
    }

    public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
    {
      var response = new ServiceResponse<FightResultDto>
      {
        Data = new FightResultDto(),
      };
      try
      {
        var characters = await this._context.Characters
                          .Include(c => c.Weapon)
                          .Include(c => c.Skills)
                          .Include(c => c.User)
                          .Where(c => request.CharacterIds.Contains(c.Id))
                          .ToListAsync();
        var defeated = false;

        while (!defeated)
        {
          foreach (var attacker in characters)
          {
            var opponents = characters.Where(c => c.Id != attacker.Id && c.User.Id != attacker.User.Id).ToList();
            var opponent = opponents[(new Random().Next(opponents.Count))];

            var damage = 0;
            var attackUsed = string.Empty;

            var useWeapon = new Random().Next(2) == 0;

            if (useWeapon)
            {
              attackUsed = attacker.Weapon.Name;
              damage = DoWeaponAttack(attacker, opponent);
            }
            else
            {
              var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
              attackUsed = skill.Name;
              damage = DoSkillAttack(attacker, opponent, skill);
            }

            response.Data.Log
                  .Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage.");
            if (opponent.HitPoints <= 0)
            {
              defeated = true;
              attacker.Victories++;
              opponent.Defeats++;
              response.Data.Log.Add($"{opponent.Name} has been defeated!");
              response.Data.Log.Add($"{attacker.Name} win with {attacker.HitPoints} HP left!");
              break;
            }
          }
        }

        characters.ForEach(c =>
        {
          c.Fights++;
          c.HitPoints = new Random().Next(1000, 1300);
        });

        await this._context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        response.Success = false;
        response.Message = ex.Message;
      }

      return response;
    }
  }
}