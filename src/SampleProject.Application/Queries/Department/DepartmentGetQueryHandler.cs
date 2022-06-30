
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Queries
{
    public class DepartmentGetQueryHandler : IRequestHandler<DepartmentGetQuery, Department>
    {
        private IReadOnlyDepartmentRepository _departmentRepository;

        public DepartmentGetQueryHandler(
            IReadOnlyDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> Handle(DepartmentGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _departmentRepository.QueryHelper()
                .Include(department => department.Location)
                .GetOneAsync(department => department.Id == request.Id);
            return entity;
        }
    }
}
