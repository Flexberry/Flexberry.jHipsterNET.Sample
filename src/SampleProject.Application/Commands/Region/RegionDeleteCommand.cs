using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class RegionDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
