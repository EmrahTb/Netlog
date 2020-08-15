using Netlog.Application.Models;
using AutoMapper;

namespace Netlog.Web.Mapper
{
    public class NetlogProfile : Profile
    {
        public NetlogProfile()
        {
            CreateMap<UserModel, UserModel>();
        }
    }
}
