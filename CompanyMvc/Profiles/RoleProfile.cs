using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PL.ViewModels;

namespace PL.Mapping
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole,RoleViewModel>().ReverseMap();
        }
    }
}
