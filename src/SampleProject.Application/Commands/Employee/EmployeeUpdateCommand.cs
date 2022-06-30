
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class EmployeeUpdateCommand : IRequest<Employee>
    {
        public Employee Employee { get; set; }
    }
}
