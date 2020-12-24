using System.Linq;
using AutoMapper;
using core_rpg_mvc.Dtos;
using core_rpg_mvc.Models;

namespace core_rpg_mvc.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Character, CharacterDto>()
                .ForMember(cd => cd.Weapon, options => options.MapFrom(c => c.Weapon.Name))
                .ForMember(cd => cd.Skills, options => options.MapFrom(c => c.Skills.Select(s => s.Skill.Name)));
            CreateMap<CharacterDto, Character>();
            CreateMap<CharacterSkill, CharacterSkillDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
        }
    }
}