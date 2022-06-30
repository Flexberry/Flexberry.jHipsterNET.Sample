
using SampleProject.Domain;
using SampleProject.Dto;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class JobGetQuery : IRequest<Job>
    {
        public long Id { get; set; }
    }
}
