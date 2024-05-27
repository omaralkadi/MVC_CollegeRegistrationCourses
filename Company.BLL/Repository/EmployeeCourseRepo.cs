using Company.BLL.Interface;
using Company.DAL.Context;
using Company.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repository
{
    public class EmployeeCourseRepo : GenericRepo<EmployeeCourse>, IEmployeeCourse
    {
        private readonly DataContext _dataContext;

        public EmployeeCourseRepo(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

    }
}
