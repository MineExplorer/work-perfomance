using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeFeedbackRepository
    {
        private DatabaseContext context;

        public EmployeeFeedbackRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IQueryable<EmployeeFeedback> GetFeedbacks()
        {
            return context.EmployeeFeedbacks.AsNoTracking();
        }
        
        public List<EmployeeFeedback> GetFeedbackForEmployee(int employeeId)
        {
            List<EmployeeFeedback> feedbackList = GetFeedbacks().Where(e => e.EmployeeId == employeeId).ToList();
            return feedbackList;
        }

        public EmployeeFeedback InsertFeedback(EmployeeFeedback employee)
        {
            var entity = context.Add(employee);
            context.SaveChanges();
            return entity.Entity;
        }

        public void DeleteFeedback(int id)
        {
            EmployeeFeedback entity = context.EmployeeFeedbacks.Find(id);
            if (entity != null)
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
