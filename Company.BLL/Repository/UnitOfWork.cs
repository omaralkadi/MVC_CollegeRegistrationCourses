﻿using Company.BLL.Interface;
using Company.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IEmployeeRepo EmployeeRepo { get; set; }
        public IDepartmentRepo DepartmentRepo { get; set; }

        public UnitOfWork(IEmployeeRepo employeeRepo, IDepartmentRepo departmentRepo, DataContext context)
        {
            EmployeeRepo = employeeRepo;
            DepartmentRepo = departmentRepo;
            _context = context;
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }


        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}