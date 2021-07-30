using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net.core.api.Dtos.Character;
using net.core.api.Models;
using net.core.api.Services.CharacterService;

namespace net.core.api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CharacterController : ControllerBase
  {
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
      this._characterService = characterService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
    {
      return Ok(await this._characterService.GetAll());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
    {
      return Ok(await this._characterService.GetById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
      return Ok(await this._characterService.AddNewObject(newCharacter));
    }
  }
}