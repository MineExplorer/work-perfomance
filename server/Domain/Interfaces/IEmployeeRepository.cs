using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Returns all employees.
        /// </summary>
        /// <returns>List of employees</returns>
        Task<List<Employee>> GetAllAsync();

        /// <summary>
        /// Returns employee by id.
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee data</returns>
        Task<Employee> GetAsync(int id);

        /// <summary>
        /// Creates employee.
        /// </summary>
        /// <param name="employee">Employee data to create</param>
        /// <returns>Created employee</returns>
        Task<Employee> CreateAsync(Employee employee);

        /// <summary>
        /// Updates employee.
        /// </summary>
        /// <param name="id">employee id</param>
        /// <param name="employee">Employee data to update</param>
        /// <returns>Updated employee</returns>
        Task<Employee> UpdateAsync(int id, Employee employee);

        /// <summary>
        /// Deletes employee by id.
        /// </summary>
        /// <param name="id">Employee id</param>
        Task DeleteAsync(int id);
    }
}
