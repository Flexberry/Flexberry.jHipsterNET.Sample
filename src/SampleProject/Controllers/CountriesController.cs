
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
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private const string EntityName = "country";
        private readonly ILogger<CountriesController> _log;
        private readonly IMediator _mediator;

        public CountriesController(ILogger<CountriesController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Country>> CreateCountry([FromBody] Country country)
        {
            _log.LogDebug($"REST request to save Country : {country}");
            if (country.Id != 0)
                throw new BadRequestAlertException("A new country cannot already have an ID", EntityName, "idexists");
            country = await _mediator.Send(new CountryCreateCommand { Country = country });
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, country.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCountry(long id, [FromBody] Country country)
        {
            _log.LogDebug($"REST request to update Country : {country}");
            if (country.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != country.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            country = await _mediator.Send(new CountryUpdateCommand { Country = country });
            return Ok(country)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, country.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetAllCountries(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Countries");
            var result = await _mediator.Send(new CountryGetAllQuery { Page = pageable });
            return Ok(result.Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Country : {id}");
            var result = await _mediator.Send(new CountryGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Country : {id}");
            await _mediator.Send(new CountryDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
