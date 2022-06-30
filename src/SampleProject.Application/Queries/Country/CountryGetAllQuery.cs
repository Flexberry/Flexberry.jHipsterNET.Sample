
using SampleProject.Domain;
using SampleProject.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class CountryGetAllQuery : IRequest<IPage<Country>>
    {
        public IPageable Page { get; set; }
    }
}
