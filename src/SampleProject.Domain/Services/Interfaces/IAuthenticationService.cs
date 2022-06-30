using System.Security.Principal;
using System.Threading.Tasks;

namespace SampleProject.Domain.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IPrincipal> Authenticate(string username, string password);
    }
}
