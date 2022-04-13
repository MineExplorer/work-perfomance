using System.Linq;
using Domain.Models;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository
    {
        private DatabaseContext context;

        public EmployeeRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IQueryable<Employee> GetEmployees()
        {
            return context.Employees.AsNoTracking().Include(e => e.ProjectEmployees).ThenInclude(e => e.Project);
        }

        public Employee InsertEmployee(Employee employee)
        {
            var entity = context.Add(employee);
            context.SaveChanges();
            return entity.Entity;
        }

        public Employee GetEmployee(int id)
        {
            Employee employee = context.Employees.Find(id);
            return employee;
        }

        public Employee UpdateEmployee(int id, Employee employee)
        {
            Employee entity = context.Employees.Find(id);
            if (entity != null)
            {
                entity.Email = employee.Email;
                entity.Name = employee.Name;
                entity.Password = employee.Password;
                entity.Seniority = employee.Seniority;
                entity.Experience = employee.Experience;
                entity.TechStack = employee.TechStack;
                entity.PermissionLevel = employee.PermissionLevel;
                context.SaveChanges();
                return entity;
            }

            return null;
        }

        public void DeleteEmployee(int id)
        {
            Employee entity = context.Employees.Find(id);
            if (entity != null)
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
