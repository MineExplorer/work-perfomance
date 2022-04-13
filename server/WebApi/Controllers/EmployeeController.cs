namespace WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.Services;
    using Application.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> logger;
        private EmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, EmployeeService employeeService)
        {
            this.logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<List<EmployeeDto>> GetAll()
        {
            try
            {
                return Ok(_employeeService.GetEmployees());
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeDto> Get(int id)
        {
            try
            {
                return Ok(_employeeService.GetEmployee(id));
            }
            catch (KeyNotFoundException)
            {
                return EmployeeNotFound(id);
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpPost]
        public ActionResult<EmployeeDto> Insert([FromBody] EmployeeCreateRequestDto employee)
        {
            try
            {
                return Ok(_employeeService.InsertEmployee(employee));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<EmployeeDto> Update(int id, [FromBody] EmployeeCreateRequestDto employee)
        {
            try
            {
                return Ok(_employeeService.UpdateEmployee(id, employee));
            }
            catch (KeyNotFoundException)
            {
                return EmployeeNotFound(id);
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        private ActionResult EmployeeNotFound(int id)
        {
            var error = new ErrorDto
            {
                Code = "NotFound",
                Message = $"Employee with id {id} not found",
                Target = "EmployeeId",
            };

            return NotFound(error);
        }

        private ActionResult InternalErrorResult(Exception ex)
        {
            var error = new ErrorDto
            {
                Code = "InternalError",
                Message = ex.Message,
            };

            return StatusCode(500, error);
        }
    }
}
