using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO.Request;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Exeptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProjectService: IProjectService
    {
        private IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectDto>> GetAllAsync()
        {
            var list = await _projectRepository.GetAllAsync();
            return list.Select(e => new ProjectDto(e)).ToList();
        }

        public async Task<ProjectDto> GetAsync(int id)
        {
            var entity = await _projectRepository.GetAsync(id);
            if (entity == null)
            {
                throw new ObjectNotFoundException($"Project with id {id} not found");
            }
            return new ProjectDto(entity).MapEmployees(entity.ProjectEmployees);
        }

        public async Task<ProjectDto> CreateAsync(ProjectCreateRequestDto project)
        {
            var entity = await _projectRepository.CreateAsync(project.ToModel());
            return new ProjectDto(entity);
        }

        public async Task<ProjectDto> UpdateAsync(int id, ProjectCreateRequestDto project)
        {
            var entity = await _projectRepository.UpdateAsync(id, project.ToModel());
            return new ProjectDto(entity);
        }

        public async Task ArchivateAsync(int id)
        {
            await _projectRepository.ArchivateAsync(id);
        }
    }
}
