
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class JobCreateCommand : IRequest<Job>
    {
        public Job Job { get; set; }
    }
}
