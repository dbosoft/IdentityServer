// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using Dbosoft.IdentityServer.Storage.Models;

namespace Dbosoft.IdentityServer.TestHost.Configuration
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            var clients = new List<Client>();

            clients.AddRange(ClientsConsole.Get());
            clients.AddRange(ClientsWeb.Get());

            return clients;
        }
    }
}