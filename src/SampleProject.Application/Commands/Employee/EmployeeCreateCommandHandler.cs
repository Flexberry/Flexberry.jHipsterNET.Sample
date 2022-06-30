
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class EmployeeCreateCommandHandler : IRequestHandler<EmployeeCreateCommand, Employee>
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeCreateCommandHandler(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> Handle(EmployeeCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _employeeRepository.CreateOrUpdateAsync(command.Employee);
            await _employeeRepository.SaveChangesAsync();
            return entity;
        }
    }
}
