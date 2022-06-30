
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class RegionCreateCommandHandler : IRequestHandler<RegionCreateCommand, Region>
    {
        private IRegionRepository _regionRepository;

        public RegionCreateCommandHandler(
            IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<Region> Handle(RegionCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _regionRepository.CreateOrUpdateAsync(command.Region);
            await _regionRepository.SaveChangesAsync();
            return entity;
        }
    }
}
