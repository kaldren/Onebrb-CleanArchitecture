using AutoMapper;
using Onebrb.Core.Dtos.Messages;
using Onebrb.Core.Dtos.User;
using Onebrb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Utils.Options
{
    public class AutoMapperOptions : Profile
    {
        public AutoMapperOptions()
        {
            CreateMap<Message, MessageDto>()
                .ReverseMap();
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
        }
    }
}
