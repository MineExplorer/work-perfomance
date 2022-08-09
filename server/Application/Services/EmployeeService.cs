using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO.Request;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Exeptions;
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

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var list = await _employeeRepository.GetAllAsync();
            return list.Select(e => new EmployeeDto(e)).ToList();
        }

        public async Task<EmployeeDto> GetAsync(int id)
        {
            var entity = await _employeeRepository.GetAsync(id);
            if (entity == null)
            {
                throw new ObjectNotFoundException($"Employee with id {id} not found");
            }
            return new EmployeeDto(entity).MapProjects(entity.ProjectEmployees);
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeCreateRequestDto employee)
        {
            var entity = await _employeeRepository.CreateAsync(employee.ToModel());
            return new EmployeeDto(entity);
        }

        public async Task<EmployeeDto> UpdateAsync(int id, EmployeeCreateRequestDto employee)
        {
            var entity = await _employeeRepository.UpdateAsync(id, employee.ToModel());
            return new EmployeeDto(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
        }
    }
}
