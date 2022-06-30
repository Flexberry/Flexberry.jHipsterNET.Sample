
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class TaskItemCreateCommandHandler : IRequestHandler<TaskItemCreateCommand, TaskItem>
    {
        private ITaskItemRepository _taskItemRepository;

        public TaskItemCreateCommandHandler(
            ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<TaskItem> Handle(TaskItemCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _taskItemRepository.CreateOrUpdateAsync(command.TaskItem);
            await _taskItemRepository.SaveChangesAsync();
            return entity;
        }
    }
}
