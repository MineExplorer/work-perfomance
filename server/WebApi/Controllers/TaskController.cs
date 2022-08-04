using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.DTO.Request;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using Domain.Exeptions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> logger;
        private ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            this.logger = logger;
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_taskService.GetTask(id));
            }
            catch (ObjectNotFoundException ex)
            {
                return TaskNotFound(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TaskDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult GetForEmployee(int employeeId)
        {
            try
            {
                return Ok(_taskService.GetTasksForEmployee(employeeId));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public IActionResult Insert([FromBody] TaskCreateRequestDto task)
        {
            try
            {
                return Ok(_taskService.InsertTask(task));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        private ActionResult TaskNotFound(ObjectNotFoundException ex)
        {
            var error = new ErrorDto
            {
                Code = "NotFound",
                Message = ex.Message,
                Target = "TaskId",
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
