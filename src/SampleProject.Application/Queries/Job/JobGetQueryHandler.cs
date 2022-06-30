
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Queries
{
    public class JobGetQueryHandler : IRequestHandler<JobGetQuery, Job>
    {
        private IReadOnlyJobRepository _jobRepository;

        public JobGetQueryHandler(
            IReadOnlyJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<Job> Handle(JobGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _jobRepository.QueryHelper()
                .Include(job => job.Tasks)
                .Include(job => job.Employee)
                .GetOneAsync(job => job.Id == request.Id);
            return entity;
        }
    }
}
