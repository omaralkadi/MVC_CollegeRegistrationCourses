using Company.DAL.Context;

namespace Company.BLL.Interface
{
    public interface IGenericRepo<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByCompositeKeyAsync(string CourseId,int id);
        Task<T> GetByCompositeKeyAsync(string CourseId,string id);
        Task<T> GetByCompositeKeyAsync(string CourseId,string id,string Name);
        Task AddAsync(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}
