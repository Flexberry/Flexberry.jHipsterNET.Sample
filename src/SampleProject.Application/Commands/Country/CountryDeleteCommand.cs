using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class CountryDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
