using MediatR;
using System.Collections.Generic;

namespace SampleProject.Application.Queries
{
    public class UserGetAuthoritiesQuery : IRequest<IEnumerable<string>>
    {
    }
}
