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
    public class CourseRepo :GenericRepo<Course>, ICourseRepo
    {
        private readonly DataContext _dataContext;

        public CourseRepo(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Course> GetAllByName(Expression<Func<Course, bool>> expression)
        {
            return _dataContext.Set<Course>().Include(e => e.AppUserCourse).Where(expression).ToList();
        }

        public async Task<Course> GetByIdStringAsync(string? id)
        {
            return await _dataContext.Set<Course>().FindAsync(id);
        }
    }
}
