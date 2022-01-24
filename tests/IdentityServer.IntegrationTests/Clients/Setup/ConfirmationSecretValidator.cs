using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using dbosoft.IdentityServer.Models;
using dbosoft.IdentityServer.Storage.Models;
using dbosoft.IdentityServer.Validation;
using dbosoft.IdentityServer.Validation.Models;

namespace IdentityServer.IntegrationTests.Clients.Setup
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