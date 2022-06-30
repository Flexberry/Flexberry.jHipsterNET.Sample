
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class TaskItemCreateCommand : IRequest<TaskItem>
    {
        public TaskItem TaskItem { get; set; }
    }
}
