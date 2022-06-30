
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class DepartmentUpdateCommandHandler : IRequestHandler<DepartmentUpdateCommand, Department>
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentUpdateCommandHandler(
            IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> Handle(DepartmentUpdateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _departmentRepository.CreateOrUpdateAsync(command.Department);
            await _departmentRepository.SaveChangesAsync();
            return entity;
        }
    }
}
