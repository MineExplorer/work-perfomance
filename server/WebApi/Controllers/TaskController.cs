using System;
using System.Collections.Generic;
using Application.DTO.Request;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public ActionResult<TaskDto> Get(int id)
        {
            try
            {
                return Ok(_taskService.GetTask(id));
            }
            catch (KeyNotFoundException)
            {
                return TaskNotFound(id);
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpGet]
        public ActionResult<List<TimeIntervalDto>> GetForEmployee(int employeeId)
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
        public ActionResult<TaskDto> Insert([FromBody] TaskCreateRequestDto task)
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

        private ActionResult TaskNotFound(int id)
        {
            var error = new ErrorDto
            {
                Code = "NotFound",
                Message = $"Task with id {id} not found",
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
