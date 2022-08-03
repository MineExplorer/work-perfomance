using System.Collections.Generic;
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
        List<EmployeeDto> GetEmployees();

        /// <summary>
        /// Returns employee by id.
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee data</returns>
        EmployeeDto GetEmployee(int id);

        /// <summary>
        /// Creates employee.
        /// </summary>
        /// <param name="employee">Employee data to create</param>
        /// <returns>Created employee</returns>
        EmployeeDto InsertEmployee(EmployeeCreateRequestDto employee);

        /// <summary>
        /// Updates employee.
        /// </summary>
        /// <param name="id">employee id</param>
        /// <param name="employee">Employee data to update</param>
        /// <returns>Updated employee</returns>
        EmployeeDto UpdateEmployee(int id, EmployeeCreateRequestDto employee);

        /// <summary>
        /// Deletes employee by id.
        /// </summary>
        /// <param name="id">Employee id</param>
        void DeleteEmployee(int id);
    }
}
