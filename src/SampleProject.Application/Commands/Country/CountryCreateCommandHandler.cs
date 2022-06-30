
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class CountryCreateCommandHandler : IRequestHandler<CountryCreateCommand, Country>
    {
        private ICountryRepository _countryRepository;

        public CountryCreateCommandHandler(
            ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Country> Handle(CountryCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _countryRepository.CreateOrUpdateAsync(command.Country);
            await _countryRepository.SaveChangesAsync();
            return entity;
        }
    }
}
