using Application.DTO.Request;
using Application.ViewModels;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IProjectService
    {
        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns>List of projects</returns>
        public List<ProjectDto> GetProjects();

        /// <summary>
        /// Returns project by id.
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>Project data</returns>
        public ProjectDto GetProject(int id);

        /// <summary>
        /// Creates project.
        /// </summary>
        /// <param name="project">Project data</param>
        /// <returns>Created project</returns>
        public ProjectDto InsertProject(ProjectCreateRequestDto project);
    }
}
