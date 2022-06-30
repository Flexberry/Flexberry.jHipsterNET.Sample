
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class TaskItemUpdateCommand : IRequest<TaskItem>
    {
        public TaskItem TaskItem { get; set; }
    }
}
