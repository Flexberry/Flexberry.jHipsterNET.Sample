
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class RegionUpdateCommandHandler : IRequestHandler<RegionUpdateCommand, Region>
    {
        private IRegionRepository _regionRepository;

        public RegionUpdateCommandHandler(
            IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<Region> Handle(RegionUpdateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _regionRepository.CreateOrUpdateAsync(command.Region);
            await _regionRepository.SaveChangesAsync();
            return entity;
        }
    }
}
