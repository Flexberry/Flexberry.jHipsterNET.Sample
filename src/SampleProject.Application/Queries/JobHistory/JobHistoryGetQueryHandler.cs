
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Queries
{
    public class JobHistoryGetQueryHandler : IRequestHandler<JobHistoryGetQuery, JobHistory>
    {
        private IReadOnlyJobHistoryRepository _jobHistoryRepository;

        public JobHistoryGetQueryHandler(
            IReadOnlyJobHistoryRepository jobHistoryRepository)
        {
            _jobHistoryRepository = jobHistoryRepository;
        }

        public async Task<JobHistory> Handle(JobHistoryGetQuery request, CancellationToken cancellationToken)
        {
            var entity = await _jobHistoryRepository.QueryHelper()
                .Include(jobHistory => jobHistory.Job)
                .Include(jobHistory => jobHistory.Department)
                .Include(jobHistory => jobHistory.Employee)
                .GetOneAsync(jobHistory => jobHistory.Id == request.Id);
            return entity;
        }
    }
}
