using Domain.Models;

namespace Application.ViewModels
{
    public class ProjectDto
    {
        public ProjectDto(Project project)
        {
            Id = project.Id;
            Title = project.Title;
        }

        public ProjectDto()
        {
        }

        public int Id { get; set; }

        public string Title { get; set; }
    }
}
