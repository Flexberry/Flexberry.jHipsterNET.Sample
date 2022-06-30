
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class JobHistoryDeleteCommandHandler : IRequestHandler<JobHistoryDeleteCommand, Unit>
    {
        private IJobHistoryRepository _jobHistoryRepository;

        public JobHistoryDeleteCommandHandler(
            IJobHistoryRepository jobHistoryRepository)
        {
            _jobHistoryRepository = jobHistoryRepository;
        }

        public async Task<Unit> Handle(JobHistoryDeleteCommand command, CancellationToken cancellationToken)
        {
            await _jobHistoryRepository.DeleteByIdAsync(command.Id);
            await _jobHistoryRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
