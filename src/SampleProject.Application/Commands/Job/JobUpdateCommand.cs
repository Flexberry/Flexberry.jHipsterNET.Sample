
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class JobUpdateCommand : IRequest<Job>
    {
        public Job Job { get; set; }
    }
}
