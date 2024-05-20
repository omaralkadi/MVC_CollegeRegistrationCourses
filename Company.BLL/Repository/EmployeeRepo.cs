using Company.BLL.Interface;
using Company.DAL.Context;
using Company.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repository
{
    public class EmployeeRepo : GenericRepo<Employee>, IEmployeeRepo
    {
        private readonly DataContext _dataContext;

        public EmployeeRepo(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Employee> GetAllByName(Expression<Func<Employee, bool>> expression)
        {
            return  _dataContext.Set<Employee>().Include(e=>e.departnment).Where(expression).ToList();
        }
    }
}
