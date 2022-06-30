using MediatR;

namespace SampleProject.Application.Commands
{
    public class AccountResetPasswordCommand : IRequest<Unit>
    {
        public string Mail { get; set; }
    }
}
