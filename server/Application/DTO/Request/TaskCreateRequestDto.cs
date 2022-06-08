using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using System;

namespace Application.DTO.Request
{
    public class TaskCreateRequestDto : IDtoMapper<Task>
    {
        public int EmployeeId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public float EstimateTime { get; set; }

        public float SpentTime { get; set; }

        public Task ToModel()
        {
            return new Task()
            {
                EmployeeId = this.EmployeeId,
                Title = this.Title,
                Description = this.Description,
                EstimateTime = this.EstimateTime,
                SpentTime = this.SpentTime
            };
        }
    }
}
