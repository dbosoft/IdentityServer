using System.Threading.Tasks;
using Dbosoft.IdentityServer.Services;
using Dbosoft.IdentityServer.Storage.Models;

namespace Dbosoft.IdentityServer.UnitTests.Common
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
