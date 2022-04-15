using Application.Interfaces;
using Domain.Enums;
using Domain.Models;
using System;

namespace Application.DTO.Request
{
    public class EmployeeFeedbackCreateRequestDto : IDtoMapper<EmployeeFeedback>
    {
        public int EmployeeId { get; set; }

        public float ClientAssesment { get; set; }

        public float PerfomanceAssesment { get; set; }

        public float CodeQualityAssesment { get; set; }

        public float TeamworkAssesment { get; set; }

        public EmployeeFeedback ToModel()
        {
            return new EmployeeFeedback()
            {
                EmployeeId = this.EmployeeId,
                ClientAssesment = this.ClientAssesment,
                PerfomanceAssesment = this.PerfomanceAssesment,
                CodeQualityAssesment = this.CodeQualityAssesment,
                TeamworkAssesment = this.TeamworkAssesment
            };
        }
    }
}
