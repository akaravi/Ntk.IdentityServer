﻿// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using Ntk.IdentityServer.Admin.BusinessLogic.Helpers;

namespace Ntk.IdentityServer.Admin.BusinessLogic.Resources
{
    public interface IClientServiceResources
    {
        ResourceMessage ClientClaimDoesNotExist();

        ResourceMessage ClientDoesNotExist();

        ResourceMessage ClientExistsKey();

        ResourceMessage ClientExistsValue();

        ResourceMessage ClientPropertyDoesNotExist();

        ResourceMessage ClientSecretDoesNotExist();
    }
}
