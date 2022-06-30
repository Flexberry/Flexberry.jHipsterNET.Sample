using MediatR;
using SampleProject.Dto;

namespace SampleProject.Application.Commands
{
    public class AccountGetQuery : IRequest<UserDto>
    {
    }
}
