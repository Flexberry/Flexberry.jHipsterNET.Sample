
using SampleProject.Domain;
using SampleProject.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class JobGetAllQuery : IRequest<IPage<Job>>
    {
        public IPageable Page { get; set; }
    }
}
