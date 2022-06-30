
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace SampleProject.Application.Queries
{
    public class LocationGetAllQueryHandler : IRequestHandler<LocationGetAllQuery, IPage<Location>>
    {
        private IReadOnlyLocationRepository _locationRepository;

        public LocationGetAllQueryHandler(
            IReadOnlyLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IPage<Location>> Handle(LocationGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _locationRepository.QueryHelper()
                .Include(location => location.Country)
                .GetPageAsync(request.Page);
            return page;
        }
    }
}
