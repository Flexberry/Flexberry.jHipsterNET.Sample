using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class JobHistoryDeleteCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}
