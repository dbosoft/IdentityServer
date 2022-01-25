using Dbosoft.IdentityServer.Models;
using Dbosoft.IdentityServer.Storage.Models;
using Dbosoft.IdentityServer.Validation.Models;

namespace Dbosoft.IdentityServer.UnitTests.Validation.Setup
{ 
    public static class ValidationExtensions
    {
        public static ClientSecretValidationResult ToValidationResult(this Client client, ParsedSecret secret = null)
        {
            return new ClientSecretValidationResult
            {
                Client = client,
                Secret = secret
            };
        }
    }
}
