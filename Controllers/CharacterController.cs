using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService characterService;
        public CharacterController(ICharacterService characterService)
        {
            this.characterService = characterService;
            
        }


        [HttpGet("GetAll")]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDtos>>>> Get(){
            return Ok(await characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ServiceResponse<GetCharacterDtos>>> GetSingle(int id){
            return Ok(await characterService.GetCharacterById(id));
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDtos>>>> Delete(int id){
            var response = await characterService.DeleteCharacter(id);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDtos>>>> AddCharacter(AddCharacterDtos newCharacter){
            
            return Ok(await characterService.AddCharacter(newCharacter));

        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDtos>>>> UpdateCharacter(UpdateCharacterDtos updateCharacter){
            
            var response = await characterService.UpdateCharacter(updateCharacter);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }
    }
}