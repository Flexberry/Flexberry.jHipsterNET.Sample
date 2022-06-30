
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class CountryUpdateCommand : IRequest<Country>
    {
        public Country Country { get; set; }
    }
}
