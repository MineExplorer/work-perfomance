using System.Linq;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private DatabaseContext context;

        public ProjectRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IQueryable<Project> GetAll()
        {
            return context.Projects.AsNoTracking().Include(e => e.ProjectEmployees).ThenInclude(e => e.Employee);
        }

        public Project Get(int id)
        {
            return GetAll().Where(p => p.Id == id).FirstOrDefault();
        }

        public Project Create(Project project)
        {
            var entity = context.Add(project);
            context.SaveChanges();
            return entity.Entity;
        }
    }
}
