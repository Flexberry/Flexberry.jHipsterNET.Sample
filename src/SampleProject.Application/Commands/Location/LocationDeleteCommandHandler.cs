
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class LocationDeleteCommandHandler : IRequestHandler<LocationDeleteCommand, Unit>
    {
        private ILocationRepository _locationRepository;

        public LocationDeleteCommandHandler(
            ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Unit> Handle(LocationDeleteCommand command, CancellationToken cancellationToken)
        {
            await _locationRepository.DeleteByIdAsync(command.Id);
            await _locationRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
