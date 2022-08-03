using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.EF;

namespace Infrastructure.Repositories
{
    public class TimeIntervalRepository: ITimeIntervalRepository
    {
        private DatabaseContext context;

        public TimeIntervalRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<TimeInterval> GetAsync(int id)
        {
            TimeInterval entity = await context.TimeIntervals.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Task with id {id} not found");
            }
            return entity;
        }
        
        public async Task<TimeInterval> CreateAsync(TimeInterval timeInterval)
        {
            context.Add(timeInterval);
            await context.SaveChangesAsync();
            return timeInterval;
        }
        
        public async Task<TimeInterval> UpdateAsync(int id, TimeInterval timeInterval)
        {
            TimeInterval entity = await context.TimeIntervals.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Task with id {entity.Id} not found");
            }

            entity.ProjectId = timeInterval.ProjectId;
            entity.Duration = timeInterval.Duration;
            entity.Description = timeInterval.Description;
            entity.Date = timeInterval.Date;
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            TimeInterval entity = await context.TimeIntervals.FindAsync(id);
            if (entity != null)
            {
                context.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public List<TimeInterval> GetAllForEmployee(int employeeId, DateTime startDate, DateTime endDate)
        {
            return context.TimeIntervals.AsNoTracking().
                Where(e => e.EmployeeId == employeeId && e.Date >= startDate && e.Date <= endDate).
                Include(e => e.Project).
                Include(e => e.WorkType).
                OrderBy(e => e.Date).
                ToList();
        }

        public Dictionary<int, List<TimeInterval>> GetForEmployeeByProjects(int employeeId, DateTime startDate, DateTime endDate)
        {
            var intervalsByProject = new Dictionary<int, List<TimeInterval>>();
            var intervals = context.TimeIntervals.AsNoTracking().
                Where(e => e.EmployeeId == employeeId && e.Date >= startDate && e.Date <= endDate);
            var employeeProjects = context.ProjectEmployees.Where(e => e.EmployeeId == employeeId).ToList();
            foreach (var record in employeeProjects)
            {
                var projectIntervals = intervals.Where(i => i.ProjectId == record.ProjectId).ToList();
                intervalsByProject.Add(record.ProjectId, projectIntervals);
            }
            return intervalsByProject;
        }

        public Dictionary<int, List<TimeInterval>> GetForProjectByEmployees(int projectId, DateTime startDate, DateTime endDate)
        {
            var intervalsByEmployee = new Dictionary<int, List<TimeInterval>>();
            var intervals = context.TimeIntervals.AsNoTracking().
                 Where(e => e.ProjectId == projectId && e.Date >= startDate && e.Date <= endDate);
            var employeeProjects = context.ProjectEmployees.Where(e => e.ProjectId == projectId).ToList();
            foreach (var record in employeeProjects)
            {
                var employeeIntervals = intervals.Where(i => i.EmployeeId == record.EmployeeId).ToList();
                intervalsByEmployee.Add(record.EmployeeId, employeeIntervals);
            }
            return intervalsByEmployee;
        }
    }
}
