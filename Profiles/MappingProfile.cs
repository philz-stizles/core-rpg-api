using AutoMapper;
using core_rpg_mvc.Dtos;
using core_rpg_mvc.Models;

namespace core_rpg_mvc.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Character, CharacterDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
        }
    }
}