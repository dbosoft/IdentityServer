// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Dbosoft.IdentityServer.ResponseHandling;
using Dbosoft.IdentityServer.ResponseHandling.Models;
using Dbosoft.IdentityServer.Validation.Models;

namespace Dbosoft.IdentityServer.UnitTests.Common
{
    internal class StubAuthorizeResponseGenerator : IAuthorizeResponseGenerator
    {
        public AuthorizeResponse Response { get; set; } = new AuthorizeResponse();

        public Task<AuthorizeResponse> CreateResponseAsync(ValidatedAuthorizeRequest request)
        {
            return Task.FromResult(Response);
        }
    }
}