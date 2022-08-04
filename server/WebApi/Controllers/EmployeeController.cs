using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.DTO.Request;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Exeptions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> logger;
        private IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
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
            catch (ObjectNotFoundException ex)
            {
                return EmployeeNotFound(ex);
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
            catch (ObjectNotFoundException ex)
            {
                return EmployeeNotFound(ex);
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

        private ActionResult EmployeeNotFound(ObjectNotFoundException ex)
        {
            var error = new ErrorDto {
                Code = "NotFound",
                Message = ex.Message,
                Target = "EmployeeId"
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
