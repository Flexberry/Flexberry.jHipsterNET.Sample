
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class RegionCreateCommand : IRequest<Region>
    {
        public Region Region { get; set; }
    }
}
