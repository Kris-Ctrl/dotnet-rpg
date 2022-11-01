using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDtos>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDtos>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDtos>>> AddCharacter(AddCharacterDtos newCharacter);
        Task<ServiceResponse<GetCharacterDtos>> UpdateCharacter(UpdateCharacterDtos updatedCharacter);
        Task<ServiceResponse<List<GetCharacterDtos>>> DeleteCharacter(int id);

    }
}