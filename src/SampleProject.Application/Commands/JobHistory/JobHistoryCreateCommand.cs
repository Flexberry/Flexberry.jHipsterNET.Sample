
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class JobHistoryCreateCommand : IRequest<JobHistory>
    {
        public JobHistory JobHistory { get; set; }
    }
}
