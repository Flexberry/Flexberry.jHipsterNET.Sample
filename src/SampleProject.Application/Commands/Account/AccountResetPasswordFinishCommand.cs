using SampleProject.Domain;
using MediatR;
using SampleProject.Dto;

namespace SampleProject.Application.Commands
{
    public class AccountResetPasswordFinishCommand : IRequest<User>
    {
        public KeyAndPasswordDto KeyAndPasswordDto { get; set; }
    }
}
