using MediatR;
using SampleProject.Dto;

namespace SampleProject.Application.Commands
{
    public class AccountChangePasswordCommand : IRequest<Unit>
    {
        public PasswordChangeDto PasswordChangeDto { get; set; }
    }
}
