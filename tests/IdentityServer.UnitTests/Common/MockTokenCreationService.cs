using System.Threading.Tasks;
using dbosoft.IdentityServer.Services;
using dbosoft.IdentityServer.Storage.Models;

namespace IdentityServer.UnitTests.Common
{
    class MockTokenCreationService : ITokenCreationService
    {
        public string Token { get; set; }

        public Task<string> CreateTokenAsync(Token token)
        {
            return Task.FromResult(Token);
        }
    }
}
