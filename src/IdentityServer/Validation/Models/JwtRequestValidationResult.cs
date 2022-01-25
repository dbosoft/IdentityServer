// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Security.Claims;

namespace Dbosoft.IdentityServer.Validation.Models
{
    /// <summary>
    /// Models the result of JWT request validation.
    /// </summary>
    public class JwtRequestValidationResult : ValidationResult
    {
        /// <summary>
        /// The validated claims from the JWT payload of a successful validated request.
        /// </summary>
        public IEnumerable<Claim> Payload { get; set; }
    }
}