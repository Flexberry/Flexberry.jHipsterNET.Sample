
using SampleProject.Domain;
using SampleProject.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class TaskItemGetAllQuery : IRequest<IPage<TaskItem>>
    {
        public IPageable Page { get; set; }
    }
}
