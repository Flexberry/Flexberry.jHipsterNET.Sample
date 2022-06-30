
using SampleProject.Domain;
using SampleProject.Dto;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class LocationGetQuery : IRequest<Location>
    {
        public long Id { get; set; }
    }
}
