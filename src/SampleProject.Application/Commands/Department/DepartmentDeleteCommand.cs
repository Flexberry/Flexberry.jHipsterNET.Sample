using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class DepartmentDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
