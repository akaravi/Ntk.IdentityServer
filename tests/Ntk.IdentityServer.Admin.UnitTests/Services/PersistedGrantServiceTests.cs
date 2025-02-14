﻿// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Threading.Tasks;
using Duende.IdentityServer.EntityFramework.Options;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Skoruba.AuditLogging.Services;
using Ntk.IdentityServer.Admin.BusinessLogic.Identity.Resources;
using Ntk.IdentityServer.Admin.BusinessLogic.Identity.Services;
using Ntk.IdentityServer.Admin.BusinessLogic.Identity.Services.Interfaces;
using Ntk.IdentityServer.Admin.EntityFramework.Identity.Repositories;
using Ntk.IdentityServer.Admin.EntityFramework.Identity.Repositories.Interfaces;
using Ntk.IdentityServer.Admin.EntityFramework.Shared.DbContexts;
using Ntk.IdentityServer.Admin.EntityFramework.Shared.Entities.Identity;
using Ntk.IdentityServer.Admin.UnitTests.Mocks;
using Xunit;

namespace Ntk.IdentityServer.Admin.UnitTests.Services
{
    public class PersistedGrantServiceTests
    {
        public PersistedGrantServiceTests()
        {
            var identityDatabaseName = Guid.NewGuid().ToString();

            _identityDbContextOptions = new DbContextOptionsBuilder<AdminIdentityDbContext>()
                .UseInMemoryDatabase(identityDatabaseName)
                .Options;
        }

        private readonly DbContextOptions<AdminIdentityDbContext> _identityDbContextOptions;

        private IdentityServerPersistedGrantDbContext GetDbContext()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(new ConfigurationStoreOptions());
            serviceCollection.AddSingleton(new OperationalStoreOptions());

            serviceCollection.AddDbContext<IdentityServerPersistedGrantDbContext>(builder => builder.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var context = serviceProvider.GetService<IdentityServerPersistedGrantDbContext>();

            return context;
        }

        private IPersistedGrantAspNetIdentityRepository GetPersistedGrantRepository(AdminIdentityDbContext identityDbContext, IdentityServerPersistedGrantDbContext context)
        {
            var persistedGrantRepository = new PersistedGrantAspNetIdentityRepository<AdminIdentityDbContext, IdentityServerPersistedGrantDbContext, UserIdentity, UserIdentityRole, string, UserIdentityUserClaim, UserIdentityUserRole, UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken>(identityDbContext, context);

            return persistedGrantRepository;
        }

        private IPersistedGrantAspNetIdentityService
            GetPersistedGrantService(IPersistedGrantAspNetIdentityRepository repository, IPersistedGrantAspNetIdentityServiceResources persistedGrantServiceResources, IAuditEventLogger auditEventLogger)
        {
            var persistedGrantService = new PersistedGrantAspNetIdentityService(repository,
                persistedGrantServiceResources, auditEventLogger);

            return persistedGrantService;
        }

        [Fact]
        public async Task GetPersistedGrantAsync()
        {
            using (var context = GetDbContext())
            {
                using (var identityDbContext = new AdminIdentityDbContext(_identityDbContextOptions))
                {
                    var persistedGrantRepository = GetPersistedGrantRepository(identityDbContext, context);

                    var localizerMock = new Mock<IPersistedGrantAspNetIdentityServiceResources>();
                    var localizer = localizerMock.Object;

                    var auditLoggerMock = new Mock<IAuditEventLogger>();
                    var auditLogger = auditLoggerMock.Object;
                    
                    var persistedGrantService = GetPersistedGrantService(persistedGrantRepository, localizer, auditLogger);

                    //Generate persisted grant
                    var persistedGrantKey = Guid.NewGuid().ToString();
                    var persistedGrant = PersistedGrantMock.GenerateRandomPersistedGrant(persistedGrantKey);

                    //Try add new persisted grant
                    await context.PersistedGrants.AddAsync(persistedGrant);
                    await context.SaveChangesAsync();

                    //Try get persisted grant
                    var persistedGrantAdded = await persistedGrantService.GetPersistedGrantAsync(persistedGrantKey);

                    //Assert
                    persistedGrantAdded.Should().BeEquivalentTo(persistedGrant);
                }
            }
        }

        [Fact]
        public async Task DeletePersistedGrantAsync()
        {
            using (var context = GetDbContext())
            {
                using (var identityDbContext = new AdminIdentityDbContext(_identityDbContextOptions))
                {
                    var persistedGrantRepository = GetPersistedGrantRepository(identityDbContext, context);

                    var localizerMock = new Mock<IPersistedGrantAspNetIdentityServiceResources>();
                    var localizer = localizerMock.Object;

                    var auditLoggerMock = new Mock<IAuditEventLogger>();
                    var auditLogger = auditLoggerMock.Object;

                    var persistedGrantService = GetPersistedGrantService(persistedGrantRepository, localizer, auditLogger);

                    //Generate persisted grant
                    var persistedGrantKey = Guid.NewGuid().ToString();
                    var persistedGrant = PersistedGrantMock.GenerateRandomPersistedGrant(persistedGrantKey);

                    //Try add new persisted grant
                    await context.PersistedGrants.AddAsync(persistedGrant);
                    await context.SaveChangesAsync();

                    //Try delete persisted grant
                    await persistedGrantService.DeletePersistedGrantAsync(persistedGrantKey);

                    var grant = await persistedGrantRepository.GetPersistedGrantAsync(persistedGrantKey);

                    //Assert
                    grant.Should().BeNull();
                }
            }
        }

        [Fact]
        public async Task DeletePersistedGrantsAsync()
        {
            using (var context = GetDbContext())
            {
                using (var identityDbContext = new AdminIdentityDbContext(_identityDbContextOptions))
                {
                    var persistedGrantRepository = GetPersistedGrantRepository(identityDbContext, context);

                    var localizerMock = new Mock<IPersistedGrantAspNetIdentityServiceResources>();
                    var localizer = localizerMock.Object;

                    var auditLoggerMock = new Mock<IAuditEventLogger>();
                    var auditLogger = auditLoggerMock.Object;

                    var persistedGrantService = GetPersistedGrantService(persistedGrantRepository, localizer, auditLogger);

                    const int subjectId = 1;

                    for (var i = 0; i < 4; i++)
                    {
                        //Generate persisted grant
                        var persistedGrantKey = Guid.NewGuid().ToString();
                        var persistedGrant = PersistedGrantMock.GenerateRandomPersistedGrant(persistedGrantKey, subjectId.ToString());

                        //Try add new persisted grant
                        await context.PersistedGrants.AddAsync(persistedGrant);
                    }

                    await context.SaveChangesAsync();

                    //Try delete persisted grant
                    await persistedGrantService.DeletePersistedGrantsAsync(subjectId.ToString());

                    var grant = await persistedGrantRepository.GetPersistedGrantsByUserAsync(subjectId.ToString());

                    //Assert
                    grant.TotalCount.Should().Be(0);
                }
            }
        }
    }
}