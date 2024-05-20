using Company.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasMany(e => e.Employee)
                .WithOne(e => e.departnment)
                .HasForeignKey(e => e.DepartmentId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
