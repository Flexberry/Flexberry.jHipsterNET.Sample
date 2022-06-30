using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class TaskItemDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
