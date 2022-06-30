
using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class EmployeeCreateCommand : IRequest<Employee>
    {
        public Employee Employee { get; set; }
    }
}
