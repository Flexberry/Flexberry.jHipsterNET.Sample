
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class JobDeleteCommandHandler : IRequestHandler<JobDeleteCommand, Unit>
    {
        private IJobRepository _jobRepository;

        public JobDeleteCommandHandler(
            IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<Unit> Handle(JobDeleteCommand command, CancellationToken cancellationToken)
        {
            await _jobRepository.DeleteByIdAsync(command.Id);
            await _jobRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
