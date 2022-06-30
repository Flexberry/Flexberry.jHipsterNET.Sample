
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class CountryUpdateCommandHandler : IRequestHandler<CountryUpdateCommand, Country>
    {
        private ICountryRepository _countryRepository;

        public CountryUpdateCommandHandler(
            ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Country> Handle(CountryUpdateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _countryRepository.CreateOrUpdateAsync(command.Country);
            await _countryRepository.SaveChangesAsync();
            return entity;
        }
    }
}
