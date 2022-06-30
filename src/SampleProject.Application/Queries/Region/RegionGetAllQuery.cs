
using SampleProject.Domain;
using SampleProject.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class RegionGetAllQuery : IRequest<IPage<Region>>
    {
        public IPageable Page { get; set; }
    }
}
