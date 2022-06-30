
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class LocationUpdateCommand : IRequest<Location>
    {
        public Location Location { get; set; }
    }
}
