using System;
using System.Linq;
using Domain.Models;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TimeIntervalRepository
    {
        private DatabaseContext context;

        public TimeIntervalRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IQueryable<TimeInterval> GetTimeIntervalsForEmployee(int employeeId)
        {
            return context.TimeIntervals.AsNoTracking().
                Where(e => e.EmployeeId == employeeId).
                Include(e => e.Project).
                Include(e => e.WorkType);
        }
        
        public IQueryable<TimeInterval> GetTimeIntervalsForEmployee(int employeeId, DateTime startDate, DateTime endDate)
        {
            return context.TimeIntervals.AsNoTracking().
                Where(e => e.EmployeeId == employeeId && e.Date >= startDate && e.Date <= endDate);
        }
        
        public IQueryable<TimeInterval> GetTimeIntervalsForProject(int projectId, DateTime startDate, DateTime endDate)
        {
            return context.TimeIntervals.AsNoTracking().
                Where(e => e.ProjectId == projectId && e.Date >= startDate && e.Date <= endDate);
        }

        public TimeInterval InsertTimeInterval(TimeInterval timeInterval)
        {
            var entity = context.Add(timeInterval);
            context.SaveChanges();
            return entity.Entity;
        }

        public TimeInterval GetTimeInterval(int id)
        {
            TimeInterval timeInterval = context.TimeIntervals.Find(id);
            return timeInterval;
        }

        public TimeInterval UpdateTimeInterval(int id, TimeInterval timeInterval)
        {
            TimeInterval entity = context.TimeIntervals.Find(id);
            if (entity != null)
            {
                entity.ProjectId = timeInterval.ProjectId;
                entity.Duration = timeInterval.Duration;
                entity.Description = timeInterval.Description;
                entity.Date = timeInterval.Date;
                context.SaveChanges();
                return entity;
            }

            return null;
        }

        public void DeleteTimeInterval(int id)
        {
            TimeInterval entity = context.TimeIntervals.Find(id);
            if (entity != null)
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
