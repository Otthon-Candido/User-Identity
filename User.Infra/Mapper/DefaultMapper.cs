using AutoMapper;
using User.Domain.Models;
using User.Infra.Entities;

namespace User.Infra.Mapper
{

    public class DefaultMapper : Profile
    {
        public DefaultMapper()
        {

            CreateMap<UserModel, UserEntity>()
            .ReverseMap();
            CreateMap<UserCreateModel, UserEntity>()
           .ReverseMap();
        }


    }

}