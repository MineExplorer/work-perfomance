using System.Linq;
using Domain.Models;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProjectRepository
    {
        private DatabaseContext context;

        public ProjectRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IQueryable<Project> GetProjects()
        {
            return context.Projects.AsNoTracking().Include(e => e.ProjectEmployees).ThenInclude(e => e.Employee);
        }

        public Project GetProject(int id)
        {
            return GetProjects().Where(p => p.Id == id).FirstOrDefault();
        }

        public Project InsertProject(Project project)
        {
            var entity = context.Add(project);
            context.SaveChanges();
            return entity.Entity;
        }
    }
}
