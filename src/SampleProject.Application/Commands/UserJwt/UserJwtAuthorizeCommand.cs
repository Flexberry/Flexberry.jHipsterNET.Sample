using MediatR;
using SampleProject.Dto;
using System.Security.Principal;

namespace SampleProject.Application.Commands
{
    public class UserJwtAuthorizeCommand : IRequest<IPrincipal>
    {
        public LoginDto LoginDto { get; set; }
    }
}
