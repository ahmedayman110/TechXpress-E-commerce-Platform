using AutoMapper;
using TechXpress.Models;
using TechXpress.DTOs;

namespace TechXpress.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDTO, User>();
            CreateMap<User, RegisterDTO>();
        }
    }
}
