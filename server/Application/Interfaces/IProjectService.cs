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
        List<ProjectDto> GetProjects();

        /// <summary>
        /// Returns project by id.
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>Project data</returns>
        ProjectDto GetProject(int id);

        /// <summary>
        /// Creates project.
        /// </summary>
        /// <param name="project">Project data</param>
        /// <returns>Created project</returns>
        ProjectDto InsertProject(ProjectCreateRequestDto project);
    }
}
