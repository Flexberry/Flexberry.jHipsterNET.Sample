
using MediatR;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using SampleProject.Domain;
using SampleProject.Crosscutting.Enums;
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
    [Route("api/job-histories")]
    [ApiController]
    public class JobHistoriesController : ControllerBase
    {
        private const string EntityName = "jobHistory";
        private readonly ILogger<JobHistoriesController> _log;
        private readonly IMediator _mediator;

        public JobHistoriesController(ILogger<JobHistoriesController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<JobHistory>> CreateJobHistory([FromBody] JobHistory jobHistory)
        {
            _log.LogDebug($"REST request to save JobHistory : {jobHistory}");
            if (jobHistory.Id != 0)
                throw new BadRequestAlertException("A new jobHistory cannot already have an ID", EntityName, "idexists");
            jobHistory = await _mediator.Send(new JobHistoryCreateCommand { JobHistory = jobHistory });
            return CreatedAtAction(nameof(GetJobHistory), new { id = jobHistory.Id }, jobHistory)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, jobHistory.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJobHistory(long id, [FromBody] JobHistory jobHistory)
        {
            _log.LogDebug($"REST request to update JobHistory : {jobHistory}");
            if (jobHistory.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != jobHistory.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            jobHistory = await _mediator.Send(new JobHistoryUpdateCommand { JobHistory = jobHistory });
            return Ok(jobHistory)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, jobHistory.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobHistory>>> GetAllJobHistories(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of JobHistories");
            var result = await _mediator.Send(new JobHistoryGetAllQuery { Page = pageable });
            return Ok(result.Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobHistory([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get JobHistory : {id}");
            var result = await _mediator.Send(new JobHistoryGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobHistory([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete JobHistory : {id}");
            await _mediator.Send(new JobHistoryDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
