﻿using Company.BLL.Interface;
using Company.DAL.Context;
using Company.DAL.Entities;
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
        }
        public void Update(T Entity)
        {
            _dataContext.Set<T>().Update(Entity);
        }

        public void Delete(T Entity)
        {
            _dataContext.Set<T>().Remove(Entity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {

                return (IEnumerable<T>) await _dataContext.Set<Employee>().Include(e => e.departnment).ToListAsync();
            }
            return await _dataContext.Set<T>().ToListAsync();

        }
    }
}
