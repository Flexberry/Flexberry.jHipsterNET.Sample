
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class EmployeeUpdateCommandHandler : IRequestHandler<EmployeeUpdateCommand, Employee>
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeUpdateCommandHandler(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> Handle(EmployeeUpdateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _employeeRepository.CreateOrUpdateAsync(command.Employee);
            await _employeeRepository.SaveChangesAsync();
            return entity;
        }
    }
}
