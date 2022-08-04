using Application.DTO.Request;
using Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProjectService
    {
        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns>List of projects</returns>
        Task<List<ProjectDto>> GetAllAsync();

        /// <summary>
        /// Returns project by id.
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>Project data</returns>
        Task<ProjectDto> GetAsync(int id);

        /// <summary>
        /// Creates project.
        /// </summary>
        /// <param name="project">Project data</param>
        /// <returns>Created project</returns>
        Task<ProjectDto> CreateAsync(ProjectCreateRequestDto project);
    }
}
