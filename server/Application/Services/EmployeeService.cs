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

    public class EmployeeService
    {
        private EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<EmployeeDto> GetEmployees()
        {
            return _employeeRepository.GetEmployees().Select(x => new EmployeeDto(x, false)).ToList();
        }

        public EmployeeDto InsertEmployee(EmployeeCreateRequestDto employee)
        {
            return new EmployeeDto(_employeeRepository.InsertEmployee(employee.ToModel()));
        }

        public EmployeeDto GetEmployee(int id)
        {
            Employee result = _employeeRepository.GetEmployee(id);
            if (result == null)
            {
                throw new KeyNotFoundException();
            }

            return new EmployeeDto(result, true);
        }

        public EmployeeDto UpdateEmployee(int id, EmployeeCreateRequestDto employee)
        {
            Employee result = _employeeRepository.UpdateEmployee(id, employee.ToModel());
            if (result == null)
            {
                throw new KeyNotFoundException();
            }

            return new EmployeeDto(result);
        }

        public void DeleteEmployee(int id)
        {
            _employeeRepository.DeleteEmployee(id);
        }
    }
}
