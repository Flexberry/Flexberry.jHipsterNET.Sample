
using SampleProject.Domain;
using SampleProject.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class LocationGetAllQuery : IRequest<IPage<Location>>
    {
        public IPageable Page { get; set; }
    }
}
