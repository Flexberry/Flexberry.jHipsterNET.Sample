
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class JobHistoryUpdateCommand : IRequest<JobHistory>
    {
        public JobHistory JobHistory { get; set; }
    }
}
