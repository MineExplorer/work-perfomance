namespace Infrastructure.EF
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
                 : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Employee> Employees { get; set; }
        
        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }

        public DbSet<TimeInterval> TimeIntervals { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<WorkType> WorkTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEmployee>().HasKey(pe => new { pe.ProjectId, pe.EmployeeId });
            modelBuilder.Entity<WorkType>().HasData(
               new { Id = 1, Name = "Разработка" },
               new { Id = 2, Name = "Тестирование" },
               new { Id = 3, Name = "Изучение" },
               new { Id = 4, Name = "Документация" },
               new { Id = 5, Name = "Коммуникация" },
               new { Id = 6, Name = "Встреча" });
        }
    }
}
