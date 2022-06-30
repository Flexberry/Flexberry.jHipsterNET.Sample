
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class RegionUpdateCommand : IRequest<Region>
    {
        public Region Region { get; set; }
    }
}
