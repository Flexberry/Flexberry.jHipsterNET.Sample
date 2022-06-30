
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class LocationUpdateCommandHandler : IRequestHandler<LocationUpdateCommand, Location>
    {
        private ILocationRepository _locationRepository;

        public LocationUpdateCommandHandler(
            ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> Handle(LocationUpdateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _locationRepository.CreateOrUpdateAsync(command.Location);
            await _locationRepository.SaveChangesAsync();
            return entity;
        }
    }
}
