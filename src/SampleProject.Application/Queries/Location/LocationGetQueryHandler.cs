
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Queries
{
    public class LocationGetQueryHandler : IRequestHandler<LocationGetQuery, Location>
    {
        private IReadOnlyLocationRepository _locationRepository;

        public LocationGetQueryHandler(
            IReadOnlyLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> Handle(LocationGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _locationRepository.QueryHelper()
                .Include(location => location.Country)
                .GetOneAsync(location => location.Id == request.Id);
            return entity;
        }
    }
}
