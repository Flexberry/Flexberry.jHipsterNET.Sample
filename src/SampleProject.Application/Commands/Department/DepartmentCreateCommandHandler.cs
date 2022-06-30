
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class DepartmentCreateCommandHandler : IRequestHandler<DepartmentCreateCommand, Department>
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentCreateCommandHandler(
            IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> Handle(DepartmentCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _departmentRepository.CreateOrUpdateAsync(command.Department);
            await _departmentRepository.SaveChangesAsync();
            return entity;
        }
    }
}
