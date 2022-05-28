// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using Ntk.IdentityServer.Admin.Api.ExceptionHandling;

namespace Ntk.IdentityServer.Admin.Api.Resources
{
    public interface IApiErrorResources
    {
        ApiError CannotSetId();
    }
}