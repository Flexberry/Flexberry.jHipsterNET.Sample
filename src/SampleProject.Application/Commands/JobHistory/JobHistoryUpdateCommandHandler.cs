
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class JobHistoryUpdateCommandHandler : IRequestHandler<JobHistoryUpdateCommand, JobHistory>
    {
        private IJobHistoryRepository _jobHistoryRepository;

        public JobHistoryUpdateCommandHandler(
            IJobHistoryRepository jobHistoryRepository)
        {
            _jobHistoryRepository = jobHistoryRepository;
        }

        public async Task<JobHistory> Handle(JobHistoryUpdateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _jobHistoryRepository.CreateOrUpdateAsync(command.JobHistory);
            await _jobHistoryRepository.SaveChangesAsync();
            return entity;
        }
    }
}
