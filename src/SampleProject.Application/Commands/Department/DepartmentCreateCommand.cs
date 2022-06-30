
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class DepartmentCreateCommand : IRequest<Department>
    {
        public Department Department { get; set; }
    }
}
