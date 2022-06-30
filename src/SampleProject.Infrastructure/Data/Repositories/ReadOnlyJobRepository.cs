using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using SampleProject.Domain;
using SampleProject.Domain.Repositories.Interfaces;
using SampleProject.Infrastructure.Data.Extensions;

namespace SampleProject.Infrastructure.Data.Repositories
{
    public class ReadOnlyJobRepository : ReadOnlyGenericRepository<Job, long>, IReadOnlyJobRepository
    {
        public ReadOnlyJobRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
