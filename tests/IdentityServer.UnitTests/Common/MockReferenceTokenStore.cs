using System;
using System.Threading.Tasks;
using Dbosoft.IdentityServer.Storage.Models;
using Dbosoft.IdentityServer.Storage.Stores;

namespace Dbosoft.IdentityServer.UnitTests.Common
{
    class MockReferenceTokenStore : IReferenceTokenStore
    {
        public Task<Token> GetReferenceTokenAsync(string handle)
        {
            throw new NotImplementedException();
        }

        public Task RemoveReferenceTokenAsync(string handle)
        {
            throw new NotImplementedException();
        }

        public Task RemoveReferenceTokensAsync(string subjectId, string clientId)
        {
            throw new NotImplementedException();
        }

        public Task<string> StoreReferenceTokenAsync(Token token)
        {
            throw new NotImplementedException();
        }
    }
}
