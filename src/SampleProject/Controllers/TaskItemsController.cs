
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
    [Route("api/task-items")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private const string EntityName = "taskItem";
        private readonly ILogger<TaskItemsController> _log;
        private readonly IMediator _mediator;

        public TaskItemsController(ILogger<TaskItemsController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<TaskItem>> CreateTaskItem([FromBody] TaskItem taskItem)
        {
            _log.LogDebug($"REST request to save TaskItem : {taskItem}");
            if (taskItem.Id != 0)
                throw new BadRequestAlertException("A new taskItem cannot already have an ID", EntityName, "idexists");
            taskItem = await _mediator.Send(new TaskItemCreateCommand { TaskItem = taskItem });
            return CreatedAtAction(nameof(GetTaskItem), new { id = taskItem.Id }, taskItem)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, taskItem.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateTaskItem(long id, [FromBody] TaskItem taskItem)
        {
            _log.LogDebug($"REST request to update TaskItem : {taskItem}");
            if (taskItem.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != taskItem.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            taskItem = await _mediator.Send(new TaskItemUpdateCommand { TaskItem = taskItem });
            return Ok(taskItem)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, taskItem.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllTaskItems(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of TaskItems");
            var result = await _mediator.Send(new TaskItemGetAllQuery { Page = pageable });
            return Ok(result.Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskItem([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get TaskItem : {id}");
            var result = await _mediator.Send(new TaskItemGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete TaskItem : {id}");
            await _mediator.Send(new TaskItemDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
