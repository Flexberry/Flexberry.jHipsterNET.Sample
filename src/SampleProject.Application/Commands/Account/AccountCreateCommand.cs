using SampleProject.Domain;
using MediatR;
using SampleProject.Dto;

namespace SampleProject.Application.Commands
{
    public class AccountCreateCommand : IRequest<User>
    {
        public ManagedUserDto ManagedUserDto { get; set; }
    }
}
