
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class JobCreateCommandHandler : IRequestHandler<JobCreateCommand, Job>
    {
        private IJobRepository _jobRepository;

        public JobCreateCommandHandler(
            IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<Job> Handle(JobCreateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _jobRepository.CreateOrUpdateAsync(command.Job);
            await _jobRepository.SaveChangesAsync();
            return entity;
        }
    }
}
