using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using SampleProject.Infrastructure.Data.Extensions;

namespace SampleProject.Infrastructure.Data.Repositories
{
    public class JobRepository : GenericRepository<Job, long>, IJobRepository
    {
        public JobRepository(IUnitOfWork context) : base(context)
        {
        }

        public override async Task<Job> CreateOrUpdateAsync(Job job)
        {
            List<Type> entitiesToBeUpdated = new List<Type>();

            await RemoveManyToManyRelationship("JobTasks", "JobsId", "TasksId", job.Id, job.Tasks.Select(x => x.Id).ToList());
            entitiesToBeUpdated.Add(typeof(TaskItem));
            return await base.CreateOrUpdateAsync(job, entitiesToBeUpdated);
        }
    }
}
