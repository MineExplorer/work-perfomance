using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Application.DTO.Request;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Exeptions;
using System.Threading.Tasks;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EmployeeDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _employeeService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _employeeService.GetAsync(id));
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateRequestDto employee)
        {
            try
            {
                return Ok(await _employeeService.CreateAsync(employee));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeCreateRequestDto employee)
        {
            try
            {
                return Ok(await _employeeService.UpdateAsync(id, employee));
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.DeleteAsync(id);
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
