using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class EmployeeDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
