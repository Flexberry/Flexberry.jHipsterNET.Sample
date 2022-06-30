
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
    [Route("api/jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private const string EntityName = "job";
        private readonly ILogger<JobsController> _log;
        private readonly IMediator _mediator;

        public JobsController(ILogger<JobsController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Job>> CreateJob([FromBody] Job job)
        {
            _log.LogDebug($"REST request to save Job : {job}");
            if (job.Id != 0)
                throw new BadRequestAlertException("A new job cannot already have an ID", EntityName, "idexists");
            job = await _mediator.Send(new JobCreateCommand { Job = job });
            return CreatedAtAction(nameof(GetJob), new { id = job.Id }, job)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, job.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJob(long id, [FromBody] Job job)
        {
            _log.LogDebug($"REST request to update Job : {job}");
            if (job.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != job.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            job = await _mediator.Send(new JobUpdateCommand { Job = job });
            return Ok(job)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, job.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAllJobs(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Jobs");
            var result = await _mediator.Send(new JobGetAllQuery { Page = pageable });
            return Ok(result.Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Job : {id}");
            var result = await _mediator.Send(new JobGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Job : {id}");
            await _mediator.Send(new JobDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
