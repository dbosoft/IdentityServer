﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Dbosoft.IdentityServer.Configuration.DependencyInjection.Options;
using Dbosoft.IdentityServer.Models.Contexts;
using Dbosoft.IdentityServer.Services;
using Dbosoft.IdentityServer.Services.Default;
using Dbosoft.IdentityServer.Stores;
using Dbosoft.IdentityServer.Stores.Default;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Dbosoft.IdentityServer.UnitTests.Common
{
    internal class MockHttpContextAccessor : IHttpContextAccessor
    {
        private HttpContext _context = new DefaultHttpContext();
        public MockAuthenticationService AuthenticationService { get; set; } = new MockAuthenticationService();

        public MockAuthenticationSchemeProvider Schemes { get; set; } = new MockAuthenticationSchemeProvider();

        public MockHttpContextAccessor(
            IdentityServerOptions options = null,
            IUserSession userSession = null,
            IMessageStore<LogoutNotificationContext> endSessionStore = null)
        {
            options = options ?? TestIdentityServerOptions.Create();

            var services = new ServiceCollection();
            services.AddSingleton(options);

            services.AddSingleton<IAuthenticationSchemeProvider>(Schemes);
            services.AddSingleton<IAuthenticationService>(AuthenticationService);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = Schemes.Default;
            });

            if (userSession == null)
            {
                services.AddScoped<IUserSession, DefaultUserSession>();
            }
            else
            {
                services.AddSingleton(userSession);
            }

            if (endSessionStore == null)
            {
                services.AddTransient<IMessageStore<LogoutNotificationContext>, ProtectedDataMessageStore<LogoutNotificationContext>>();
            }
            else
            {
                services.AddSingleton(endSessionStore);
            }

            _context.RequestServices = services.BuildServiceProvider();
        }

        public HttpContext HttpContext
        {
            get
            {
                return _context;
            }

            set
            {
                _context = value;
            }
        }
    }
}
