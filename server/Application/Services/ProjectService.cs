using System.Collections.Generic;
using System.Linq;
using Application.DTO.Request;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class ProjectService: IProjectService
    {
        private IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public List<ProjectDto> GetProjects()
        {
            return _projectRepository.GetAll().Select(x => new ProjectDto(x, false)).ToList();
        }

        public ProjectDto GetProject(int id)
        {
            Project result = _projectRepository.Get(id);
            return new ProjectDto(result, true);
        }

        public ProjectDto InsertProject(ProjectCreateRequestDto project)
        {
            return new ProjectDto(_projectRepository.Create(project.ToModel()));
        }
    }
}
