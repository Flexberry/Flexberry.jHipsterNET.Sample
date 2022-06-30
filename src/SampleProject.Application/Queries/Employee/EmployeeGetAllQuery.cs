
using SampleProject.Domain;
using SampleProject.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class EmployeeGetAllQuery : IRequest<IPage<Employee>>
    {
        public IPageable Page { get; set; }
    }
}
