using SampleProject.Domain;
using MediatR;
using SampleProject.Dto;

namespace SampleProject.Application.Commands
{
    public class UserUpdateCommand : IRequest<User>
    {
        public UserDto UserDto { get; set; }
    }
}
