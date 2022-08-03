namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request;
    using Application.Interfaces;
    using Application.ViewModels;
    using Domain.Models;
    using Infrastructure.Repositories;

    public class ProjectService: IProjectService
    {
        private ProjectRepository _projectRepository;

        public ProjectService(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public List<ProjectDto> GetProjects()
        {
            return _projectRepository.GetProjects().Select(x => new ProjectDto(x, false)).ToList();
        }

        public ProjectDto GetProject(int id)
        {
            Project result = _projectRepository.GetProject(id);
            if (result == null)
            {
                throw new KeyNotFoundException();
            }

            return new ProjectDto(result, true);
        }

        public ProjectDto InsertProject(ProjectCreateRequestDto project)
        {
            return new ProjectDto(_projectRepository.InsertProject(project.ToModel()));
        }
    }
}
