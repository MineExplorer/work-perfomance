using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO.Request;
using Application.ViewModels;

namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Returns all employees.
        /// </summary>
        /// <returns>List of employees</returns>
        Task<List<EmployeeDto>> GetAllAsync();

        /// <summary>
        /// Returns employee by id.
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee data</returns>
        Task<EmployeeDto> GetAsync(int id);

        /// <summary>
        /// Creates employee.
        /// </summary>
        /// <param name="employee">Employee data to create</param>
        /// <returns>Created employee</returns>
        Task<EmployeeDto> CreateAsync(EmployeeCreateRequestDto employee);

        /// <summary>
        /// Updates employee.
        /// </summary>
        /// <param name="id">employee id</param>
        /// <param name="employee">Employee data to update</param>
        /// <returns>Updated employee</returns>
        Task<EmployeeDto> UpdateAsync(int id, EmployeeCreateRequestDto employee);

        /// <summary>
        /// Deletes employee by id.
        /// </summary>
        /// <param name="id">Employee id</param>
        Task DeleteAsync(int id);
    }
}
