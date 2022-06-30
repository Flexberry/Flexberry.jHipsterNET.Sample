using SampleProject.Domain;
using SampleProject.Domain.Services.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Commands
{
    public class AccountActivateCommandHandler : IRequestHandler<AccountActivateCommand, User>
    {
        private readonly IUserService _userService;

        public AccountActivateCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<User> Handle(AccountActivateCommand command, CancellationToken cancellationToken)
        {
            return _userService.ActivateRegistration(command.Key);
        }
    }
}
