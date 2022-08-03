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
        public List<EmployeeDto> GetEmployees();

        /// <summary>
        /// Returns employee by id.
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee data</returns>
        public EmployeeDto GetEmployee(int id);

        /// <summary>
        /// Creates employee.
        /// </summary>
        /// <param name="employee">Employee data to create</param>
        /// <returns>Created employee</returns>
        public EmployeeDto InsertEmployee(EmployeeCreateRequestDto employee);

        /// <summary>
        /// Updates employee.
        /// </summary>
        /// <param name="id">employee id</param>
        /// <param name="employee">Employee data to update</param>
        /// <returns>Updated employee</returns>
        public EmployeeDto UpdateEmployee(int id, EmployeeCreateRequestDto employee);

        /// <summary>
        /// Deletes employee by id.
        /// </summary>
        /// <param name="id">Employee id</param>
        public void DeleteEmployee(int id);
    }
}
