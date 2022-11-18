using AutoMapper;
using Dto;
using Dto.User;
using Entity;

namespace Service
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<User, SignInModel>().ReverseMap();
            CreateMap<User, SignUpModel>().ReverseMap();
        }
    }
}