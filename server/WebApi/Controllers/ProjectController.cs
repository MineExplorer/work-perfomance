using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProjectDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _projectService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _projectService.GetAsync(id));
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> Create([FromBody] ProjectCreateRequestDto project)
        {
            try
            {
                return Ok(await _projectService.CreateAsync(project));
            }
            catch (Exception ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> Update(int id, [FromBody] ProjectCreateRequestDto project)
        {
            try
            {
                return Ok(await _projectService.UpdateAsync(id, project));
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDto))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _projectService.DeleteAsync(id);
                return Ok();
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
