using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dbosoft.IdentityServer.Models;
using Dbosoft.IdentityServer.Services;
using Microsoft.IdentityModel.Tokens;

namespace Dbosoft.IdentityServer.UnitTests.Common
{
    class MockKeyMaterialService : IKeyMaterialService
    {
        public List<SigningCredentials> SigningCredentials = new List<SigningCredentials>();
        public List<SecurityKeyInfo> ValidationKeys = new List<SecurityKeyInfo>();

        public Task<IEnumerable<SigningCredentials>> GetAllSigningCredentialsAsync()
        {
            return Task.FromResult(SigningCredentials.AsEnumerable());
        }

        public Task<SigningCredentials> GetSigningCredentialsAsync(IEnumerable<string> allowedAlgorithms = null)
        {
            return Task.FromResult(SigningCredentials.FirstOrDefault());
        }

        public Task<IEnumerable<SecurityKeyInfo>> GetValidationKeysAsync()
        {
            return Task.FromResult(ValidationKeys.AsEnumerable());
        }
    }
}
