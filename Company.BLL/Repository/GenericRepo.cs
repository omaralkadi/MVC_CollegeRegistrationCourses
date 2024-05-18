using Company.BLL.Interface;
using Company.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Company.BLL.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly DataContext _dataContext;

        public GenericRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(T Entity)
        {
            await _dataContext.Set<T>().AddAsync(Entity);
            await _dataContext.SaveChangesAsync();
        }
        public void Update(T Entity)
        {
            _dataContext.Set<T>().Update(Entity);
            _dataContext.SaveChangesAsync();
        }

        public void Delete(T Entity)
        {
            _dataContext.Set<T>().Remove(Entity);
            _dataContext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

    }
}
