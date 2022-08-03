using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private DatabaseContext context;

        public TaskRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IQueryable<TaskEntity> GetAll()
        {
            return context.Tasks.AsNoTracking();
        }

        public TaskEntity Get(int id)
        {
            return context.Tasks.Find(id);
        }
        
        public List<TaskEntity> GetAllForEmployee(int employeeId)
        {
            List<TaskEntity> feedbackList = GetAll().Where(e => e.EmployeeId == employeeId).ToList();
            return feedbackList;
        }

        public TaskEntity Create(TaskEntity task)
        {
            var entity = context.Add(task);
            context.SaveChanges();
            return entity.Entity;
        }

        public void Delete(int id)
        {
            TaskEntity entity = context.Tasks.Find(id);
            if (entity != null)
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
