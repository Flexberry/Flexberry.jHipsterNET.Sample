
using SampleProject.Domain;
using SampleProject.Dto;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class JobHistoryGetQuery : IRequest<JobHistory>
    {
        public long Id { get; set; }
    }
}
