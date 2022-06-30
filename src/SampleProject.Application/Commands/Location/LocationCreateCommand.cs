
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class LocationCreateCommand : IRequest<Location>
    {
        public Location Location { get; set; }
    }
}
