
using MediatR;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using SampleProject.Domain;
using SampleProject.Crosscutting.Exceptions;
using SampleProject.Web.Extensions;
using SampleProject.Web.Filters;
using SampleProject.Web.Rest.Utilities;
using SampleProject.Application.Queries;
using SampleProject.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace SampleProject.Controllers
{
    [Authorize]
    [Route("api/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private const string EntityName = "department";
        private readonly ILogger<DepartmentsController> _log;
        private readonly IMediator _mediator;

        public DepartmentsController(ILogger<DepartmentsController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Department>> CreateDepartment([FromBody] Department department)
        {
            _log.LogDebug($"REST request to save Department : {department}");
            if (department.Id != 0)
                throw new BadRequestAlertException("A new department cannot already have an ID", EntityName, "idexists");
            department = await _mediator.Send(new DepartmentCreateCommand { Department = department });
            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, department.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateDepartment(long id, [FromBody] Department department)
        {
            _log.LogDebug($"REST request to update Department : {department}");
            if (department.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != department.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            department = await _mediator.Send(new DepartmentUpdateCommand { Department = department });
            return Ok(department)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, department.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Departments");
            var result = await _mediator.Send(new DepartmentGetAllQuery { Page = pageable });
            return Ok(result.Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Department : {id}");
            var result = await _mediator.Send(new DepartmentGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Department : {id}");
            await _mediator.Send(new DepartmentDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
