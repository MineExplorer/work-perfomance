namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Request;
    using Application.ViewModels;
    using Domain.Enums;
    using Domain.Models;
    using Infrastructure.Repositories;

    public class EmployeeFeedbackService
    {
        private EmployeeFeedbackRepository _employeeFeedbackRepository;

        public EmployeeFeedbackService(EmployeeFeedbackRepository employeeRepository)
        {
            _employeeFeedbackRepository = employeeRepository;
        }

        public List<EmployeeDto> GetEmployees()
        {
            return _employeeFeedbackRepository.GetEmployees().Select(x => new EmployeeDto(x)).ToList();
        }

        public EmployeeFeedbackDto InsertFeedback(EmployeeFeedbackCreateRequestDto feedback)
        {
            return new EmployeeFeedbackDto(_employeeFeedbackRepository.InsertFeedback(feedback.ToModel()));
        }

        public EmployeeFeedbackDto GetEmployee(int id)
        {
            EmployeeFeedback result = _employeeFeedbackRepository.GetEmployee(id);
            if (result == null)
            {
                throw new KeyNotFoundException();
            }

            return new EmployeeFeedbackDto(result);
        }

        public void DeleteFeedback(int id)
        {
            _employeeFeedbackRepository.DeleteFeedback(id);
        }
    }
}
