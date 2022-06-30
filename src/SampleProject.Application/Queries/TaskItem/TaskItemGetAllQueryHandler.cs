
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace SampleProject.Application.Queries
{
    public class TaskItemGetAllQueryHandler : IRequestHandler<TaskItemGetAllQuery, IPage<TaskItem>>
    {
        private IReadOnlyTaskItemRepository _taskItemRepository;

        public TaskItemGetAllQueryHandler(
            IReadOnlyTaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<IPage<TaskItem>> Handle(TaskItemGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _taskItemRepository.QueryHelper()
                .GetPageAsync(request.Page);
            return page;
        }
    }
}
