
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class CountryCreateCommand : IRequest<Country>
    {
        public Country Country { get; set; }
    }
}
