using SampleProject.Domain;
using MediatR;
using SampleProject.Dto;

namespace SampleProject.Application.Commands
{
    public class UserCreateCommand : IRequest<User>
    {
        public UserDto UserDto { get; set; }
    }
}
