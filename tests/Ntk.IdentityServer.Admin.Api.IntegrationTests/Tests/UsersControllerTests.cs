﻿// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Ntk.IdentityServer.Admin.Api.IntegrationTests.Common;
using Ntk.IdentityServer.Admin.Api.IntegrationTests.Tests.Base;
using Xunit;

namespace Ntk.IdentityServer.Admin.Api.IntegrationTests.Tests
{
    public class UsersControllerTests : BaseClassFixture
    {
        public UsersControllerTests(TestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetRolesAsAdmin()
        {
            SetupAdminClaimsViaHeaders();

            var response = await Client.GetAsync("api/users");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetRolesWithoutPermissions()
        {
            Client.DefaultRequestHeaders.Clear();

            var response = await Client.GetAsync("api/users");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);

            //The redirect to login
            response.Headers.Location.ToString().Should().Contain(AuthenticationConsts.AccountLoginPage);
        }
    }
}