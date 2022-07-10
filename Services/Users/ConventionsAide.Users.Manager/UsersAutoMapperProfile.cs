using AutoMapper;
using ConventionsAide.Users.Contracts;
using ConventionsAide.Users.Domain;

namespace ConventionsAide.Users.Manager
{
    public class UsersAutoMapperProfile : Profile
    {
        public UsersAutoMapperProfile()
        {
            CreateMap<CreateUserRequestDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
