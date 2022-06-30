
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
    [Route("api/regions")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private const string EntityName = "region";
        private readonly ILogger<RegionsController> _log;
        private readonly IMediator _mediator;

        public RegionsController(ILogger<RegionsController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Region>> CreateRegion([FromBody] Region region)
        {
            _log.LogDebug($"REST request to save Region : {region}");
            if (region.Id != 0)
                throw new BadRequestAlertException("A new region cannot already have an ID", EntityName, "idexists");
            region = await _mediator.Send(new RegionCreateCommand { Region = region });
            return CreatedAtAction(nameof(GetRegion), new { id = region.Id }, region)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, region.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion(long id, [FromBody] Region region)
        {
            _log.LogDebug($"REST request to update Region : {region}");
            if (region.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != region.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            region = await _mediator.Send(new RegionUpdateCommand { Region = region });
            return Ok(region)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, region.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetAllRegions(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Regions");
            var result = await _mediator.Send(new RegionGetAllQuery { Page = pageable });
            return Ok(result.Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegion([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Region : {id}");
            var result = await _mediator.Send(new RegionGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Region : {id}");
            await _mediator.Send(new RegionDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
