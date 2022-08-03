using System.Collections.Generic;
using System.Linq;
using Application.DTO.Request;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<EmployeeDto> GetEmployees()
        {
            return _employeeRepository.GetAll().Select(x => new EmployeeDto(x, false)).ToList();
        }

        public EmployeeDto GetEmployee(int id)
        {
            Employee result = _employeeRepository.Get(id);
            if (result == null)
            {
                throw new KeyNotFoundException();
            }

            return new EmployeeDto(result, true);
        }

        public EmployeeDto InsertEmployee(EmployeeCreateRequestDto employee)
        {
            return new EmployeeDto(_employeeRepository.Create(employee.ToModel()));
        }

        public EmployeeDto UpdateEmployee(int id, EmployeeCreateRequestDto employee)
        {
            Employee result = _employeeRepository.Update(id, employee.ToModel());
            if (result == null)
            {
                throw new KeyNotFoundException();
            }

            return new EmployeeDto(result);
        }

        public void DeleteEmployee(int id)
        {
            _employeeRepository.Delete(id);
        }
    }
}
