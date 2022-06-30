
using SampleProject.Domain;
using SampleProject.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class DepartmentGetAllQuery : IRequest<IPage<Department>>
    {
        public IPageable Page { get; set; }
    }
}
