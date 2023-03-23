using AutoMapper;
using ShoritifierMVC.Models;

namespace ShoritifierMVC.Configuration
{
    public class AutoMapperUserDto : Profile
    {
        public AutoMapperUserDto() => CreateMap<User, UserDto>()
            .ReverseMap();
    }
}
