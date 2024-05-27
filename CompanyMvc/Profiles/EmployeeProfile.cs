using AutoMapper;
using Company.DAL.Entities;

namespace CompanyMvc.ViewModels
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeVM>().ReverseMap();

        }
    }
}
