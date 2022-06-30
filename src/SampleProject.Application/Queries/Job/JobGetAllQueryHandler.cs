
using SampleProject.Domain;
using SampleProject.Dto;
using SampleProject.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace SampleProject.Application.Queries
{
    public class JobGetAllQueryHandler : IRequestHandler<JobGetAllQuery, IPage<Job>>
    {
        private IReadOnlyJobRepository _jobRepository;

        public JobGetAllQueryHandler(
            IReadOnlyJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IPage<Job>> Handle(JobGetAllQuery request, CancellationToken cancellationToken)
        {
            var page = await _jobRepository.QueryHelper()
                .Include(job => job.Tasks)
                .Include(job => job.Employee)
                .GetPageAsync(request.Page);
            return page;
        }
    }
}
