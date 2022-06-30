
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Queries
{
    public class TaskItemGetQueryHandler : IRequestHandler<TaskItemGetQuery, TaskItem>
    {
        private IReadOnlyTaskItemRepository _taskItemRepository;

        public TaskItemGetQueryHandler(
            IReadOnlyTaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<TaskItem> Handle(TaskItemGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _taskItemRepository.QueryHelper()
                .GetOneAsync(taskItem => taskItem.Id == request.Id);
            return entity;
        }
    }
}
