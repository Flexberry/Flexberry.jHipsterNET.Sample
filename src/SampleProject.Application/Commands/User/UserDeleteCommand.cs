using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class UserDeleteCommand : IRequest<Unit>
    {
        public string Login { get; set; }
    }
}
