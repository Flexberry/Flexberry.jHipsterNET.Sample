
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
    [Route("api/locations")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private const string EntityName = "location";
        private readonly ILogger<LocationsController> _log;
        private readonly IMediator _mediator;

        public LocationsController(ILogger<LocationsController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Location>> CreateLocation([FromBody] Location location)
        {
            _log.LogDebug($"REST request to save Location : {location}");
            if (location.Id != 0)
                throw new BadRequestAlertException("A new location cannot already have an ID", EntityName, "idexists");
            location = await _mediator.Send(new LocationCreateCommand { Location = location });
            return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, location)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, location.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateLocation(long id, [FromBody] Location location)
        {
            _log.LogDebug($"REST request to update Location : {location}");
            if (location.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != location.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            location = await _mediator.Send(new LocationUpdateCommand { Location = location });
            return Ok(location)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, location.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Locations");
            var result = await _mediator.Send(new LocationGetAllQuery { Page = pageable });
            return Ok(result.Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Location : {id}");
            var result = await _mediator.Send(new LocationGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Location : {id}");
            await _mediator.Send(new LocationDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
