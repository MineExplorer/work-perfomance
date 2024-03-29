﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns>List of projects</returns>
        Task<List<Project>> GetAllAsync();

        // <summary>
        /// Returns project by id.
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>Project data</returns>
        Task<Project> GetAsync(int id);

        /// <summary>
        /// Creates project.
        /// </summary>
        /// <param name="project">Project data</param>
        /// <returns>Created project</returns>
        Task<Project> CreateAsync(Project project);

        /// <summary>
        /// Updates project/
        /// </summary>
        /// <param name="id">Project id</param>
        /// <param name="project">Project data to update</param>
        /// <returns>Updated project</returns>
        Task<Project> UpdateAsync(int id, Project project);

        /// <summary>
        /// Archivates project by id.
        /// </summary>
        /// <param name="id">Project id</param>
        Task ArchivateAsync(int id);
    }
}
