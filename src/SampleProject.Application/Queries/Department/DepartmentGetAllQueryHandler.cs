
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace SampleProject.Application.Queries
{
    public class DepartmentGetAllQueryHandler : IRequestHandler<DepartmentGetAllQuery, IPage<Department>>
    {
        private IReadOnlyDepartmentRepository _departmentRepository;

        public DepartmentGetAllQueryHandler(
            IReadOnlyDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IPage<Department>> Handle(DepartmentGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _departmentRepository.QueryHelper()
                .Include(department => department.Location)
                .GetPageAsync(request.Page);
            return page;
        }
    }
}
