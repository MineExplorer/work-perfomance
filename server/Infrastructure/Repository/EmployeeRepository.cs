using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Domain.Exeptions;
using Infrastructure.EF;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private DatabaseContext context;

        public EmployeeRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IQueryable<Employee> GetAll()
        {
            return context.Employees.AsNoTracking().Include(e => e.ProjectEmployees).ThenInclude(e => e.Project);
        }

        public Employee Get(int id)
        {
            Employee employee = GetAll().Where(e => e.Id == id).FirstOrDefault();
            if (employee == null)
            {
                throw new ObjectNotFoundException($"Employee with id {id} not found");
            }
            return employee;
        }

        public Employee Create(Employee employee)
        {
            var entity = context.Add(employee);
            context.SaveChanges();
            return entity.Entity;
        }

        public Employee Update(int id, Employee employee)
        {
            Employee entity = context.Employees.Find(id);
            if (entity == null)
            {
                throw new ObjectNotFoundException($"Employee with id {id} not found");
            }

            entity.Email = employee.Email;
            entity.Name = employee.Name;
            entity.Password = employee.Password;
            entity.Seniority = employee.Seniority;
            entity.Experience = employee.Experience;
            entity.HourlyRate = employee.HourlyRate;
            entity.PermissionLevel = employee.PermissionLevel;
            context.Update(entity);
            context.SaveChanges();

            return entity;
        }

        public void Delete(int id)
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
