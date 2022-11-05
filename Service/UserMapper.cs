using AutoMapper;
using Dto;
using Entity;

namespace Service
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserRegisterDto>().ReverseMap();
        }
    }
}