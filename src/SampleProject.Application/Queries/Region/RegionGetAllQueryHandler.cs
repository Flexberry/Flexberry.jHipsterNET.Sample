
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace SampleProject.Application.Queries
{
    public class RegionGetAllQueryHandler : IRequestHandler<RegionGetAllQuery, IPage<Region>>
    {
        private IReadOnlyRegionRepository _regionRepository;

        public RegionGetAllQueryHandler(
            IReadOnlyRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<IPage<Region>> Handle(RegionGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _regionRepository.QueryHelper()
                .GetPageAsync(request.Page);
            return page;
        }
    }
}
