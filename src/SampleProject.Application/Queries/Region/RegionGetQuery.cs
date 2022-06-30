
using SampleProject.Domain;
using SampleProject.Dto;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class RegionGetQuery : IRequest<Region>
    {
        public long Id { get; set; }
    }
}
