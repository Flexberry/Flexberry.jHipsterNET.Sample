using System.Security.Claims;

namespace SampleProject.Test.Setup
{
    public class MockClaimsPrincipalProvider
    {
        public MockClaimsPrincipalProvider(ClaimsPrincipal user)
        {
            User = user;
        }

        public ClaimsPrincipal User { get; }
    }
}
