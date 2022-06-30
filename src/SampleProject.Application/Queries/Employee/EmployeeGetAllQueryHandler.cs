
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace SampleProject.Application.Queries
{
    public class EmployeeGetAllQueryHandler : IRequestHandler<EmployeeGetAllQuery, IPage<Employee>>
    {
        private IReadOnlyEmployeeRepository _employeeRepository;

        public EmployeeGetAllQueryHandler(
            IReadOnlyEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IPage<Employee>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _employeeRepository.QueryHelper()
                .Include(employee => employee.Manager)
                .Include(employee => employee.Department)
                .GetPageAsync(request.Page);
            return page;
        }
    }
}
