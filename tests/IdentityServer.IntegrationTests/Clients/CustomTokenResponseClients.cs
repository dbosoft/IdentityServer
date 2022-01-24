// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using IdentityModel;
using IdentityModel.Client;
using IdentityServer.IntegrationTests.Clients.Setup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace IdentityServer.IntegrationTests.Clients
{
    public class CustomTokenResponseClients
    {
        private const string TokenEndpoint = "https://server/connect/token";

        private readonly HttpClient _client;

        public CustomTokenResponseClients()
        {
            var builder = new WebHostBuilder()
                .UseStartup<StartupWithCustomTokenResponses>();
            var server = new TestServer(builder);

            _client = server.CreateClient();
        }

        [Fact]
        public async Task Resource_owner_success_should_return_custom_response()
        {
            var response = await _client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = TokenEndpoint,
                ClientId = "roclient",
                ClientSecret = "secret",

                UserName = "bob",
                Password = "bob",
                Scope = "api1"
            });

            GetField<string>(response,"string_value").Should().Be("some_string");
            GetField<int>(response, "int_value").Should().Be(42);


            GetField<string>(response, "identity_token").Should().BeNull();
            GetField<string>(response, "refresh_token").Should().BeNull();
            GetField<string>(response, "error").Should().BeNull();
            GetField<string>(response, "error_description").Should().BeNull();
            GetField<string>(response, "token_type").Should().Be("Bearer");
            GetField<long>(response, "expires_in").Should().NotBe(0);


            var responseDto = GetField<CustomResponseDto>(response, "dto");
            responseDto.Should().NotBeNull();
            var dto = CustomResponseDto.Create;

            responseDto.string_value.Should().Be(dto.string_value);
            responseDto.int_value.Should().Be(dto.int_value);
            responseDto.nested.string_value.Should().Be(dto.nested.string_value);
            responseDto.nested.int_value.Should().Be(dto.nested.int_value);


            // token client response
            response.IsError.Should().Be(false);
            response.ExpiresIn.Should().Be(3600);
            response.TokenType.Should().Be("Bearer");
            response.IdentityToken.Should().BeNull();
            response.RefreshToken.Should().BeNull();


            // token content
            var payload = GetPayload<JsonElement>(response);
            payload.Count().Should().Be(12);
            payload["iss"].GetString().Should().Be("https://idsvr4");
            payload["client_id"].GetString().Should().Be("roclient");
            payload["sub"].GetString().Should().Be("bob");
            payload["idp"].GetString().Should().Be("local");
            payload["aud"].GetString().Should().Be("api");

            var scopes = payload["scope"].EnumerateArray();
            scopes.First().ToString().Should().Be("api1");

            var amr = payload["amr"].EnumerateArray();
            amr.Count().Should().Be(1);
            amr.First().ToString().Should().Be("password");
        }

        [Fact]
        public async Task Resource_owner_failure_should_return_custom_error_response()
        {
            var response = await _client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = TokenEndpoint,
                ClientId = "roclient",
                ClientSecret = "secret",

                UserName = "bob",
                Password = "invalid",
                Scope = "api1"
            });

            GetField<string>(response, "string_value").Should().Be("some_string");
            GetField<int>(response, "int_value").Should().Be(42);

            GetField<string>(response, "identity_token").Should().BeNull();
            GetField<string>(response, "refresh_token").Should().BeNull();
            GetField<string>(response, "error").Should().Be("invalid_grant");
            GetField<string>(response, "error_description").Should().NotBeNullOrWhiteSpace();
            GetField<string>(response, "token_type").Should().BeNull();
            GetField<string>(response, "expires_in").Should().BeNull();

            var responseDto = GetField<CustomResponseDto>(response, "dto");
            responseDto.Should().NotBeNull();
            var dto = CustomResponseDto.Create;

            responseDto.string_value.Should().Be(dto.string_value);
            responseDto.int_value.Should().Be(dto.int_value);
            responseDto.nested.string_value.Should().Be(dto.nested.string_value);
            responseDto.nested.int_value.Should().Be(dto.nested.int_value);



            // token client response
            response.IsError.Should().Be(true);
            response.Error.Should().Be("invalid_grant");
            response.ErrorDescription.Should().Be("invalid_credential");
            response.ExpiresIn.Should().Be(0);
            response.TokenType.Should().BeNull();
            response.IdentityToken.Should().BeNull();
            response.RefreshToken.Should().BeNull();
        }

        [Fact]
        public async Task Extension_grant_success_should_return_custom_response()
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
                    { "outcome", "succeed"}
                }
            });


            GetField<string>(response, "string_value").Should().Be("some_string");
            GetField<int>(response, "int_value").Should().Be(42);

            GetField<string>(response, "identity_token").Should().BeNull();
            GetField<string>(response, "refresh_token").Should().BeNull();
            GetField<string>(response, "error").Should().BeNull();
            GetField<string>(response, "error_description").Should().BeNull();
            GetField<string>(response, "token_type").Should().Be("Bearer");
            GetField<long>(response, "expires_in").Should().NotBe(0);

            var responseDto = GetField<CustomResponseDto>(response, "dto");
            responseDto.Should().NotBeNull();
            var dto = CustomResponseDto.Create;

            responseDto.string_value.Should().Be(dto.string_value);
            responseDto.int_value.Should().Be(dto.int_value);
            responseDto.nested.string_value.Should().Be(dto.nested.string_value);
            responseDto.nested.int_value.Should().Be(dto.nested.int_value);


            // token client response
            response.IsError.Should().Be(false);
            response.ExpiresIn.Should().Be(3600);
            response.TokenType.Should().Be("Bearer");
            response.IdentityToken.Should().BeNull();
            response.RefreshToken.Should().BeNull();

            // token content
            var payload = GetPayload<JsonElement>(response);
            payload.Count().Should().Be(12);
            payload["iss"].GetString().Should().Be("https://idsvr4");
            payload["client_id"].GetString().Should().Be("client.custom");
            payload["sub"].GetString().Should().Be("bob");
            payload["idp"].GetString().Should().Be("local");
            payload["aud"].GetString().Should().Be("api");

            var scopes = payload["scope"].EnumerateArray();
            scopes.First().ToString().Should().Be("api1");

            var amr = payload["amr"].EnumerateArray();
            amr.Count().Should().Be(1);
            amr.First().ToString().Should().Be("custom");

        }

        [Fact]
        public async Task Extension_grant_failure_should_return_custom_error_response()
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
                    { "outcome", "fail"}
                }
            });


            GetField<string>(response, "string_value").Should().Be("some_string");
            GetField<int>(response, "int_value").Should().Be(42);

            GetField<string>(response, "identity_token").Should().BeNull();
            GetField<string>(response, "refresh_token").Should().BeNull();
            GetField<string>(response, "error").Should().Be("invalid_grant");
            GetField<string>(response, "error_description").Should().NotBeNullOrWhiteSpace();
            GetField<string>(response, "token_type").Should().BeNull();
            GetField<string>(response, "expires_in").Should().BeNull();

            var responseDto = GetField<CustomResponseDto>(response, "dto");
            responseDto.Should().NotBeNull();
            var dto = CustomResponseDto.Create;

            responseDto.string_value.Should().Be(dto.string_value);
            responseDto.int_value.Should().Be(dto.int_value);
            responseDto.nested.string_value.Should().Be(dto.nested.string_value);
            responseDto.nested.int_value.Should().Be(dto.nested.int_value);


            // token client response
            response.IsError.Should().Be(true);
            response.Error.Should().Be("invalid_grant");
            response.ErrorDescription.Should().Be("invalid_credential");
            response.ExpiresIn.Should().Be(0);
            response.TokenType.Should().BeNull();
            response.IdentityToken.Should().BeNull();
            response.RefreshToken.Should().BeNull();
        }

        private static T GetField<T>(ProtocolResponse response, string fieldName)
        {
            var element = response.Json.TryGetValue(fieldName);
            if (element.ValueKind == JsonValueKind.Undefined)
                return default;

            return element.Deserialize<T>();
        }

        private static Dictionary<string, T> GetPayload<T>(TokenResponse response)
        {
            var token = response.AccessToken.Split('.').Skip(1).Take(1).First();
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, T>>(
                Encoding.UTF8.GetString(Base64Url.Decode(token)));

            return dictionary;
        }
    }
}