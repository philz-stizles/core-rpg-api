using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using core_rpg_mvc.Data;
using core_rpg_mvc.Dtos;
using core_rpg_mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace core_rpg_mvc.Services
{
    public interface ICharacterService
    {
        Task<ServiceResponse<CharacterDto[]>> GetAll();
        Task<ServiceResponse<CharacterDto>> Get(int id);
        Task<ServiceResponse<object>> Create(CharacterDto dto);
        Task<ServiceResponse<object>> Update(int id, CharacterDto dto);
        Task<ServiceResponse<object>> Delete(int id);
        Task<ServiceResponse<CharacterDto>> AddCharacterSkill(CharacterSkillDto dto);
    }

    public class CharacterService : ICharacterService
    {
        private readonly DataContext _ctx;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(DataContext ctx, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<object>> Create(CharacterDto dto)
        {
            var newCharacter = _mapper.Map<Character>(dto);
            newCharacter.UserId = UserId();
            await _ctx.Characters.AddAsync(newCharacter);
            await _ctx.SaveChangesAsync();
            return new ServiceResponse<object> { Status = true, Message = "Successful" };
        }

        public async Task<ServiceResponse<object>> Delete(int id)
        {
            var existingCharacter = await _ctx.Characters
                .Where(c => c.Id == id &&  c.UserId == UserId())
                .FirstOrDefaultAsync();
            if (existingCharacter == null) throw new System.Exception("Character does not exist");
            
            _ctx.Characters.Remove(existingCharacter);
            await _ctx.SaveChangesAsync();
            return new ServiceResponse<object> { Status = true, Message = "Successful" };
        }

        public async Task<ServiceResponse<CharacterDto>> Get(int id)
        {
            var existingCharacter = await _ctx.Characters
                .Where(c => c.Id == id &&  c.UserId == UserId())
                .FirstOrDefaultAsync();
            if (existingCharacter == null) throw new System.Exception("Character does not exist");

            return new ServiceResponse<CharacterDto> { Status = true, Message = "Successful", Data = _mapper.Map<CharacterDto>(existingCharacter) };
        }

        public async Task<ServiceResponse<CharacterDto[]>> GetAll()
        {
            var characters = await _ctx.Characters
                .Where(c => c.UserId == UserId())
                .Select(c => _mapper.Map<CharacterDto>(c))
                .ToArrayAsync();
            return new ServiceResponse<CharacterDto[]> { Status = true, Message = "Successful", Data = characters };
        }

        public async Task<ServiceResponse<object>> Update(int id, CharacterDto dto)
        {
            var existingCharacter = await _ctx.Characters
                .Where(c => c.Id == id &&  c.UserId == UserId())
                .FirstOrDefaultAsync();
            if (existingCharacter == null) throw new System.Exception("Character does not exist");

            existingCharacter.Name = dto.Name;

            await _ctx.SaveChangesAsync();
            return new ServiceResponse<object> { Status = true, Message = "Successful" };
        }

        private Task<ServiceResponse<object>> Exists(int name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<CharacterDto>> AddCharacterSkill(CharacterSkillDto dto)
        {
            var existingCharacter = await _ctx.Characters
                .FirstOrDefaultAsync(c => c.Id == dto.CharacterId &&  c.UserId == UserId());
            if (existingCharacter == null) throw new System.Exception("Character does not exist");
            

            var existingSkill = await _ctx.Skills
                .FirstOrDefaultAsync(c => c.Id == dto.SkillId);
            if (existingSkill == null) throw new System.Exception("Skill does not exist");

            await _ctx.CharacterSkills.AddAsync(new CharacterSkill 
            { 
                Character = existingCharacter, 
                Skill = existingSkill
            });
            await _ctx.SaveChangesAsync();

            return new ServiceResponse<CharacterDto> { Status = true, Message = "Successful", Data = _mapper.Map<CharacterDto>(existingCharacter) };
        }

        private int UserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string UserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
    }
}