using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos;
using dotnet_rpg.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            this._mapper = mapper;
            _context = context;
        }




        public async Task<ServiceResponse<List<GetCharacterDtos>>> AddCharacter(AddCharacterDtos newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDtos>>();
            Character character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDtos>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDtos>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDtos>> response = new ServiceResponse<List<GetCharacterDtos>>();

            try
            {
                Character character = await _context.Characters.FirstAsync(c => c.Id == id);
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                response.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDtos>(c)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDtos>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDtos>>();
            var dbCharacter = await _context.Characters.ToListAsync();
            response.Data = dbCharacter.Select(c => _mapper.Map<GetCharacterDtos>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDtos>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDtos>();
            var dbcharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDtos>(dbcharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDtos>> UpdateCharacter(UpdateCharacterDtos updatedCharacter)
        {
            ServiceResponse<GetCharacterDtos> response = new ServiceResponse<GetCharacterDtos>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);



                character.Name = updatedCharacter.Name;
                character.HitPoint = updatedCharacter.HitPoint;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDtos>(character);
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