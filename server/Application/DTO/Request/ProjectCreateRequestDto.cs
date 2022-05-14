using System;
using Application.Interfaces;
using Domain.Models;

namespace Application.DTO.Request
{
    public class ProjectCreateRequestDto : IDtoMapper<Project>
    {
        public string Title { get; set; }

        public Project ToModel()
        {
            return new Project
            {
                Title = this.Title
            };
        }
    }
}
