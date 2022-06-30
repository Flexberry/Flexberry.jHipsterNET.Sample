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
    public class TaskItemRepository : GenericRepository<TaskItem, long>, ITaskItemRepository
    {
        public TaskItemRepository(IUnitOfWork context) : base(context)
        {
        }

        public override async Task<TaskItem> CreateOrUpdateAsync(TaskItem taskItem)
        {
            List<Type> entitiesToBeUpdated = new List<Type>();
            return await base.CreateOrUpdateAsync(taskItem, entitiesToBeUpdated);
        }
    }
}
