// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Dbosoft.IdentityServer.IntegrationTests.Clients.Setup;
using FluentAssertions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Dbosoft.IdentityServer.IntegrationTests.Clients
{
    public class CustomTokenRequestValidatorClient
    {
        private const string TokenEndpoint = "https://server/connect/token";

        private readonly HttpClient _client;

        public CustomTokenRequestValidatorClient()
        {
            var val = new TestCustomTokenRequestValidator();
            Startup.CustomTokenRequestValidator = val;

            var builder = new WebHostBuilder()
                .UseStartup<Startup>();
            var server = new TestServer(builder);

            _client = server.CreateClient();
        }

        [Fact]
        public async Task Client_credentials_request_should_contain_custom_response()
        {
            var response = await _client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            GetField<string>(response, "custom").Should().Be("custom");
        }

        [Fact]
        public async Task Resource_owner_credentials_request_should_contain_custom_response()
        {
            var response = await _client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = TokenEndpoint,

                ClientId = "roclient",
                ClientSecret = "secret",
                Scope = "api1",

                UserName = "bob",
                Password = "bob"
            });

            GetField<string>(response, "custom").Should().Be("custom");
        }

        [Fact]
        public async Task Refreshing_a_token_should_contain_custom_response()
        {
            var response = await _client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = TokenEndpoint,

                ClientId = "roclient",
                ClientSecret = "secret",
                Scope = "api1 offline_access",

                UserName = "bob",
                Password = "bob"
            });

            response = await _client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = TokenEndpoint,
                ClientId = "roclient",
                ClientSecret = "secret",

                RefreshToken = response.RefreshToken
            });

            GetField<string>(response, "custom").Should().Be("custom");
        }

        [Fact]
        public async Task Extension_grant_request_should_contain_custom_response()
        {
            var response = await _client.RequestTokenAsync(new TokenRequest
            {
                Address = TokenEndpoint,
                GrantType = "custom",
                ClientId = "client.custom",
                ClientSecret = "secret",

                Parameters =
                {
                    { "scope", "api1" },
                    { "custom_credential", "custom credential"}
                }
            });

            GetField<string>(response, "custom").Should().Be("custom");
        }

        private static T GetField<T>(ProtocolResponse response, string field)
        {
            return response.Json.GetProperty(field).Deserialize<T>();
        }
    }
}