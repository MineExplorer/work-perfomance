using System.Linq;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Returns all employees.
        /// </summary>
        /// <returns>List of employees</returns>
        IQueryable<Employee> GetAll();

        /// <summary>
        /// Returns employee by id.
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee data</returns>
        Employee Get(int id);

        /// <summary>
        /// Creates employee.
        /// </summary>
        /// <param name="employee">Employee data to create</param>
        /// <returns>Created employee</returns>
        Employee Create(Employee employee);

        /// <summary>
        /// Updates employee.
        /// </summary>
        /// <param name="id">employee id</param>
        /// <param name="employee">Employee data to update</param>
        /// <returns>Updated employee</returns>
        Employee Update(int id, Employee employee);

        /// <summary>
        /// Deletes employee by id.
        /// </summary>
        /// <param name="id">Employee id</param>
        void Delete(int id);
    }
}
