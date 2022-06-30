using SampleProject.Domain;
using MediatR;

namespace SampleProject.Application.Commands
{
    public class AccountActivateCommand : IRequest<User>
    {
        public string Key { get; set; }
    }
}
