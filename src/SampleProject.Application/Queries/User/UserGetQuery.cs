using SampleProject.Dto;
using MediatR;

namespace SampleProject.Application.Queries
{
    public class UserGetQuery : IRequest<UserDto>
    {
        public string Login { get; set; }
    }
}
