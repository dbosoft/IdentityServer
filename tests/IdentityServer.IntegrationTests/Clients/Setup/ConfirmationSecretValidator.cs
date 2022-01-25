using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dbosoft.IdentityServer.Models;
using Dbosoft.IdentityServer.Storage.Models;
using Dbosoft.IdentityServer.Validation;
using Dbosoft.IdentityServer.Validation.Models;

namespace Dbosoft.IdentityServer.IntegrationTests.Clients.Setup
{
    public class ConfirmationSecretValidator : ISecretValidator
    {
        public Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret)
        {
            if (secrets.Any())
            {
                if (secrets.First().Type == "confirmation.test")
                {
                    var cnf = new Dictionary<string, string>
                    {
                        { "x5t#S256", "foo" }
                    };

                    var result = new SecretValidationResult
                    {
                        Success = true,
                        Confirmation = JsonSerializer.Serialize(cnf)
                    };

                    return Task.FromResult(result);
                }
            }

            return Task.FromResult(new SecretValidationResult { Success = false });
        }
    }
}