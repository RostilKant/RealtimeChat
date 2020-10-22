using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Entities
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<UserForAuthenticationDto, User>();
        }
    }
}