using WebApiCore.Models;
using AutoMapper;
namespace WebApiCore
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<User, UserDto>();
        }
    }
}
