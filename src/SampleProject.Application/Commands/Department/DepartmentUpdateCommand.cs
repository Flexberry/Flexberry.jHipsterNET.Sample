
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class DepartmentUpdateCommand : IRequest<Department>
    {
        public Department Department { get; set; }
    }
}
