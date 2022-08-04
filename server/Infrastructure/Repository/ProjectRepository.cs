using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.EF;
using Domain.Exeptions;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private DatabaseContext context;

        public ProjectRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await context.Projects.ToListAsync();
        }

        public async Task<Project> GetAsync(int id)
        {
            return await context.Projects.Include(e => e.ProjectEmployees).ThenInclude(e => e.Employee).
                Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Project> CreateAsync(Project project)
        {
            context.Add(project);
            await context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> UpdateAsync(int id, Project project)
        {
            Project entity = await context.Projects.FindAsync(id);
            if (entity == null)
            {
                throw new ObjectNotFoundException($"Project with id {id} not found");
            }
            entity.Title = project.Title;
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            Project entity = await context.Projects.FindAsync(id);
            if (entity != null)
            {
                context.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
