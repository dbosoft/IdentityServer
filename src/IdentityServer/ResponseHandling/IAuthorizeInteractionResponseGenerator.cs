// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Dbosoft.IdentityServer.Models.Messages;
using Dbosoft.IdentityServer.ResponseHandling.Models;
using Dbosoft.IdentityServer.Validation.Models;

namespace Dbosoft.IdentityServer.ResponseHandling
{
    /// <summary>
    /// Interface for determining if user must login or consent when making requests to the authorization endpoint.
    /// </summary>
    public interface IAuthorizeInteractionResponseGenerator
    {
        /// <summary>
        /// Processes the interaction logic.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="consent">The consent.</param>
        /// <returns></returns>
        Task<InteractionResponse> ProcessInteractionAsync(ValidatedAuthorizeRequest request, ConsentResponse consent = null);
    }
}