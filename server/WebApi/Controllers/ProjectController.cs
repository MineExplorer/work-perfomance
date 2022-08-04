using System;
using System.Collections.Generic;
using Application.DTO.Request;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Exeptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> logger;
        private IProjectService _projectService;

        public ProjectController(ILogger<ProjectController> logger, IProjectService projectService)
        {
            this.logger = logger;
            _projectService = projectService;
        }

        [HttpGet]
        public ActionResult<List<ProjectDto>> GetAll()
        {
            try
            {
                return Ok(_projectService.GetProjects());
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ProjectDto> Get(int id)
        {
            try
            {
                return Ok(_projectService.GetProject(id));
            }
            catch (ObjectNotFoundException ex)
            {
                return ProjectNotFound(ex);
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpPost]
        public ActionResult<ProjectDto> Insert([FromBody] ProjectCreateRequestDto project)
        {
            try
            {
                return Ok(_projectService.InsertProject(project));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        private ActionResult ProjectNotFound(ObjectNotFoundException ex)
        {
            var error = new ErrorDto
            {
                Code = "NotFound",
                Message = ex.Message,
                Target = "ProjectId",
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
