namespace Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.ViewModels;
    using Domain.Models;
    using Infrastructure.Repositories;

    public class ProjectService
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
    }
}
