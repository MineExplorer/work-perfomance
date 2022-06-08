using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TaskRepository
    {
        private DatabaseContext context;

        public TaskRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IQueryable<Task> GetTasks()
        {
            return context.Tasks.AsNoTracking();
        }

        public Task GetTask(int id)
        {
            return context.Tasks.Find(id);
        }
        
        public List<Task> GetTasksForEmployee(int employeeId)
        {
            List<Task> feedbackList = GetTasks().Where(e => e.EmployeeId == employeeId).ToList();
            return feedbackList;
        }

        public Task InsertTask(Task task)
        {
            var entity = context.Add(task);
            context.SaveChanges();
            return entity.Entity;
        }

        public void DeleteTask(int id)
        {
            Task entity = context.Tasks.Find(id);
            if (entity != null)
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
