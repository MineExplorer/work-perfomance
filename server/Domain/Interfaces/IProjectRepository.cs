using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Interfaces
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns>List of projects</returns>
        public IQueryable<Project> GetAll();

        // <summary>
        /// Returns project by id.
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>Project data</returns>
        public Project Get(int id);

        /// <summary>
        /// Creates project.
        /// </summary>
        /// <param name="project">Project data</param>
        /// <returns>Created project</returns>
        public Project Create(Project project);
    }
}
