
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Queries
{
    public class RegionGetQueryHandler : IRequestHandler<RegionGetQuery, Region>
    {
        private IReadOnlyRegionRepository _regionRepository;

        public RegionGetQueryHandler(
            IReadOnlyRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<Region> Handle(RegionGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _regionRepository.QueryHelper()
                .GetOneAsync(region => region.Id == request.Id);
            return entity;
        }
    }
}
