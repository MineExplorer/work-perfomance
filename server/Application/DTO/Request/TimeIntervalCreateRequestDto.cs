using System;
using Application.Interfaces;
using Domain.Models;

namespace Application.DTO.Request
{
    public class TimeIntervalCreateRequestDto : IDtoMapper<TimeInterval>
    {
        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        public int WorkTypeId { get; set; }

        public float Duration { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public TimeInterval ToModel()
        {
            return new TimeInterval
            {
                EmployeeId = this.EmployeeId,
                ProjectId = this.ProjectId,
                WorkTypeId = this.WorkTypeId,
                Duration = this.Duration,
                Description = this.Description,
                Date = DateTime.Parse(this.Date)
            };
        }
    }
}
