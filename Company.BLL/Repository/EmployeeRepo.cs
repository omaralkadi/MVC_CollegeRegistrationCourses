using Company.BLL.Interface;
using Company.DAL.Context;
using Company.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repository
{
    public class EmployeeRepo : GenericRepo<Employee>, IEmployeeRepo
    {
        public EmployeeRepo(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
