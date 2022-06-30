using SampleProject.Domain;
using MediatR;
using SampleProject.Dto;
using System.Security.Claims;

namespace SampleProject.Application.Commands
{
    public class AccountGetAuthenticatedQuery : IRequest<string>
    {
        public ClaimsPrincipal User { get; set; }
    }
}
