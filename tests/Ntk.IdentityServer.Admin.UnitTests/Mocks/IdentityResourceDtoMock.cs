﻿// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using Bogus;
using Ntk.IdentityServer.Admin.BusinessLogic.Dtos.Configuration;
using Ntk.IdentityServer.Admin.EntityFramework.Constants;

namespace Ntk.IdentityServer.Admin.UnitTests.Mocks
{
    public static class IdentityResourceDtoMock
    {
        public static Faker<IdentityResourceDto> GetIdentityResourceFaker(int id)
        {
            var fakerIdentityResource = new Faker<IdentityResourceDto>()
                .RuleFor(o => o.Name, f => Guid.NewGuid().ToString())
                .RuleFor(o => o.Id, id)
                .RuleFor(o => o.Description, f => f.Random.Words(f.Random.Number(1, 5)))
                .RuleFor(o => o.DisplayName, f => f.Random.Words(f.Random.Number(1, 5)))
                .RuleFor(o => o.Enabled, f => f.Random.Bool())
                .RuleFor(o => o.Emphasize, f => f.Random.Bool())
                .RuleFor(o => o.ShowInDiscoveryDocument, f => f.Random.Bool())
                .RuleFor(o => o.Required, f => f.Random.Bool())                
                .RuleFor(o => o.UserClaims, f => Enumerable.Range(1, f.Random.Int(1, 10)).Select(x => f.PickRandom(ClientConsts.GetStandardClaims())).ToList());

            return fakerIdentityResource;
        }

        public static IdentityResourceDto GenerateRandomIdentityResource(int id)
        {
            var identityResource = GetIdentityResourceFaker(id).Generate();

            return identityResource;
        }

	    public static IdentityResourcePropertiesDto GenerateRandomIdentityResourceProperty(int id, int identityResourceId)
	    {
		    var identityResourcePropertyFaker = IdentityResourcePropertyFaker(id, identityResourceId);

		    var propertyTesting = identityResourcePropertyFaker.Generate();

		    return propertyTesting;
	    }

	    public static Faker<IdentityResourcePropertiesDto> IdentityResourcePropertyFaker(int id, int identityResourceId)
	    {
		    var identityResourcePropertyFaker = new Faker<IdentityResourcePropertiesDto>()
			    .StrictMode(false)
			    .RuleFor(o => o.IdentityResourcePropertyId, id)
			    .RuleFor(o => o.Key, f => Guid.NewGuid().ToString())
			    .RuleFor(o => o.Value, f => Guid.NewGuid().ToString())
			    .RuleFor(o => o.IdentityResourceId, identityResourceId);

		    return identityResourcePropertyFaker;
	    }
	}
}
