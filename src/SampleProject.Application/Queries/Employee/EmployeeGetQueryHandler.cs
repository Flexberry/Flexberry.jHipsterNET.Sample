
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Queries
{
    public class EmployeeGetQueryHandler : IRequestHandler<EmployeeGetQuery, Employee>
    {
        private IReadOnlyEmployeeRepository _employeeRepository;

        public EmployeeGetQueryHandler(
            IReadOnlyEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> Handle(EmployeeGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _employeeRepository.QueryHelper()
                .Include(employee => employee.Manager)
                .Include(employee => employee.Department)
                .GetOneAsync(employee => employee.Id == request.Id);
            return entity;
        }
    }
}
