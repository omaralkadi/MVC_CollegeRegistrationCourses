using Company.DAL.Entities;
using System.Linq.Expressions;

namespace Company.BLL.Interface
{
    public interface IEmployeeRepo : IGenericRepo<Employee>
    {
        IEnumerable<Employee> GetAllByName(Expression<Func<Employee, bool>> expression);
    }
}
