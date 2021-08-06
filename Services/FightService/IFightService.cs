using System.Collections.Generic;
using System.Threading.Tasks;
using net.core.api.Dtos.Fight;
using net.core.api.Models;

namespace net.core.api.Services.FightService
{
  public interface IFightService
  {
    Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
    Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
    Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);
    Task<ServiceResponse<List<HighscoreDto>>> GetHighscore();
  }
}