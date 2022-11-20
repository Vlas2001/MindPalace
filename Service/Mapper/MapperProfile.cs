using AutoMapper;
using Dto;
using Dto.Statistics;
using Dto.User;
using Entity;

namespace Service
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<User, SignInModel>().ReverseMap();
            CreateMap<User, SignUpModel>().ReverseMap();
            CreateMap<Entity.Statistics, StatisticDto>().ReverseMap();
        }
    }
}