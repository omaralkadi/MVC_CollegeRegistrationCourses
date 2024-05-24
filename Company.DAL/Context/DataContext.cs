using Company.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Company.DAL.Context
{
    public class DataContext : IdentityDbContext<AppUser>
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

            modelBuilder.Entity<AppUserCourse>()
                .HasKey(e => new { e.CourseId, e.UserId });

            modelBuilder.Entity<AppUserCourse>().HasOne(e => e.Course).
                WithMany(e => e.AppUserCourse).
                HasForeignKey(e => e.CourseId).
                IsRequired(false).
                OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<AppUserCourse>().HasOne(e => e.AppUser).
                WithMany(e => e.AppUserCourse).
                HasForeignKey(e => e.UserId).
                IsRequired(false).
                OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
