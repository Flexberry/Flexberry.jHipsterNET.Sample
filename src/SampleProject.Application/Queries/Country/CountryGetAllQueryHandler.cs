
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace SampleProject.Application.Queries
{
    public class CountryGetAllQueryHandler : IRequestHandler<CountryGetAllQuery, IPage<Country>>
    {
        private IReadOnlyCountryRepository _countryRepository;

        public CountryGetAllQueryHandler(
            IReadOnlyCountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IPage<Country>> Handle(CountryGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _countryRepository.QueryHelper()
                .Include(country => country.Region)
                .GetPageAsync(request.Page);
            return page;
        }
    }
}
