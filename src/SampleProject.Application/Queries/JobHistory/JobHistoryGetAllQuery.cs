
using SampleProject.Domain;
using SampleProject.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class JobHistoryGetAllQuery : IRequest<IPage<JobHistory>>
    {
        public IPageable Page { get; set; }
    }
}
