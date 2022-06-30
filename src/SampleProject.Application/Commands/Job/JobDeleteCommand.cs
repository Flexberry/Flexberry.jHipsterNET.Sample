using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class JobDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
