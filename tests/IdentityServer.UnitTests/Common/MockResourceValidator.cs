using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dbosoft.IdentityServer.Validation;
using dbosoft.IdentityServer.Validation.Models;

namespace IdentityServer.UnitTests.Common
{
    class MockResourceValidator : IResourceValidator
    {
        public ResourceValidationResult Result { get; set; } = new ResourceValidationResult();

        public Task<IEnumerable<ParsedScopeValue>> ParseRequestedScopesAsync(IEnumerable<string> scopeValues)
        {
            return Task.FromResult(scopeValues.Select(x => new ParsedScopeValue(x)));
        }

        public Task<ResourceValidationResult> ValidateRequestedResourcesAsync(ResourceValidationRequest request)
        {
            return Task.FromResult(Result);
        }
    }
}
