using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dbosoft.IdentityServer.Validation;
using Dbosoft.IdentityServer.Validation.Contexts;

namespace Dbosoft.IdentityServer.TestHost.Extensions
{
    public class ParameterizedScopeTokenRequestValidator : ICustomTokenRequestValidator
    {
        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var transaction = context.Result.ValidatedRequest.ValidatedResources.ParsedScopes.FirstOrDefault(x => x.ParsedName == "transaction");
            if (transaction?.ParsedParameter != null)
            {
                context.Result.ValidatedRequest.ClientClaims.Add(new Claim(transaction.ParsedName, transaction.ParsedParameter));
            }

            return Task.CompletedTask;
        }
    }
}