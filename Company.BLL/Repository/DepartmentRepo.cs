using Company.BLL.Interface;
using Company.DAL.Context;
using Company.DAL.Entities;

namespace Company.BLL.Repository
{
    public class DepartmentRepo : GenericRepo<Department>, IDepartmentRepo
    {
        public DepartmentRepo(DataContext dataContext) : base(dataContext)
        {

        }

    }
}
