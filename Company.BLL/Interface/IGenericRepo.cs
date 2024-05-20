namespace Company.BLL.Interface
{
    public interface IGenericRepo<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}
