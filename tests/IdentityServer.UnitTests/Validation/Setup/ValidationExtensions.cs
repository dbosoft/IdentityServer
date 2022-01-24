using dbosoft.IdentityServer.Models;
using dbosoft.IdentityServer.Storage.Models;
using dbosoft.IdentityServer.Validation.Models;

namespace IdentityServer.UnitTests.Validation.Setup
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
