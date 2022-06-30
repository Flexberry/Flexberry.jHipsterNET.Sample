
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace SampleProject.Application.Queries
{
    public class JobHistoryGetAllQueryHandler : IRequestHandler<JobHistoryGetAllQuery, IPage<JobHistory>>
    {
        private IReadOnlyJobHistoryRepository _jobHistoryRepository;

        public JobHistoryGetAllQueryHandler(
            IReadOnlyJobHistoryRepository jobHistoryRepository)
        {
            _jobHistoryRepository = jobHistoryRepository;
        }

        public async Task<IPage<JobHistory>> Handle(JobHistoryGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _jobHistoryRepository.QueryHelper()
                .Include(jobHistory => jobHistory.Job)
                .Include(jobHistory => jobHistory.Department)
                .Include(jobHistory => jobHistory.Employee)
                .GetPageAsync(request.Page);
            return page;
        }
    }
}
