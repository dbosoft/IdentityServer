﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Threading.Tasks;
using dbosoft.IdentityServer.ResponseHandling.Models;
using dbosoft.IdentityServer.Validation.Models;

namespace dbosoft.IdentityServer.ResponseHandling
{
    /// <summary>
    /// Interface the token response generator
    /// </summary>
    public interface ITokenResponseGenerator
    {
        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <returns></returns>
        Task<TokenResponse> ProcessAsync(TokenRequestValidationResult validationResult);
    }
}