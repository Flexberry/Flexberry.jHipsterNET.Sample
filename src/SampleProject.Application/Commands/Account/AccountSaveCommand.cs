using MediatR;
using SampleProject.Dto;
using System.Security.Claims;

namespace SampleProject.Application.Commands
{
    public class AccountSaveCommand : IRequest<Unit>
    {
        public ClaimsPrincipal User { get; set; }
        public UserDto UserDto { get; set; }
    }
}
