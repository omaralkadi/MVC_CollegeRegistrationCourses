using Company.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interface
{
    public interface ICourseRepo:IGenericRepo<Course>
    {
        public IEnumerable<Course> GetAllByName(Expression<Func<Course, bool>> expression);

        Task<Course> GetByIdStringAsync(string? id);

    }
}
