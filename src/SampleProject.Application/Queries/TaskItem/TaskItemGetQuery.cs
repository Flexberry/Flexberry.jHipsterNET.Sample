
using SampleProject.Domain;
using SampleProject.Dto;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class TaskItemGetQuery : IRequest<TaskItem>
    {
        public long Id { get; set; }
    }
}
