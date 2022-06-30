
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class TaskItemDeleteCommandHandler : IRequestHandler<TaskItemDeleteCommand, Unit>
    {
        private ITaskItemRepository _taskItemRepository;

        public TaskItemDeleteCommandHandler(
            ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<Unit> Handle(TaskItemDeleteCommand command, CancellationToken cancellationToken)
        {
            await _taskItemRepository.DeleteByIdAsync(command.Id);
            await _taskItemRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
