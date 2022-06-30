
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Queries
{
    public class CountryGetQueryHandler : IRequestHandler<CountryGetQuery, Country>
    {
        private IReadOnlyCountryRepository _countryRepository;

        public CountryGetQueryHandler(
            IReadOnlyCountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Country> Handle(CountryGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _countryRepository.QueryHelper()
                .Include(country => country.Region)
                .GetOneAsync(country => country.Id == request.Id);
            return entity;
        }
    }
}
