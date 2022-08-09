using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Employee>> GetAllAsync()
        {
            return await context.Employees.ToListAsync();
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await context.Employees.Include(e => e.ProjectEmployees).ThenInclude(e => e.Project).
                Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await context.AddAsync(employee);
            await context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateAsync(int id, Employee employee)
        {
            Employee entity = await context.Employees.FindAsync(id);
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
            entity.WorkDayDuration = employee.WorkDayDuration;
            context.Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            Employee entity = await context.Employees.FindAsync(id);
            if (entity != null)
            {
                context.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
