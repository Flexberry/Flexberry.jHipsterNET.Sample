
using SampleProject.Domain;
using SampleProject.Dto;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class CountryGetQuery : IRequest<Country>
    {
        public long Id { get; set; }
    }
}
