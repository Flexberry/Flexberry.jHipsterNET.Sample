
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
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private const string EntityName = "employee";
        private readonly ILogger<EmployeesController> _log;
        private readonly IMediator _mediator;

        public EmployeesController(ILogger<EmployeesController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            _log.LogDebug($"REST request to save Employee : {employee}");
            if (employee.Id != 0)
                throw new BadRequestAlertException("A new employee cannot already have an ID", EntityName, "idexists");
            employee = await _mediator.Send(new EmployeeCreateCommand { Employee = employee });
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, employee.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateEmployee(long id, [FromBody] Employee employee)
        {
            _log.LogDebug($"REST request to update Employee : {employee}");
            if (employee.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != employee.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            employee = await _mediator.Send(new EmployeeUpdateCommand { Employee = employee });
            return Ok(employee)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, employee.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Employees");
            var result = await _mediator.Send(new EmployeeGetAllQuery { Page = pageable });
            return Ok(result.Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Employee : {id}");
            var result = await _mediator.Send(new EmployeeGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Employee : {id}");
            await _mediator.Send(new EmployeeDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
