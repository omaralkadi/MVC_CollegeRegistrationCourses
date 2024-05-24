using AutoMapper;
using Company.DAL.Entities;
using CompanyMvc.ViewModels;

namespace CompanyMvc.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserVM>().ReverseMap();
        }
    }
}
