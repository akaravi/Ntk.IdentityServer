﻿// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using Ntk.IdentityServer.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

namespace Ntk.IdentityServer.Admin.BusinessLogic.Identity.Dtos.Identity.Base
{
    public class BaseUserRolesDto<TKey> : IBaseUserRolesDto
    {
        public TKey UserId { get; set; }

        public TKey RoleId { get; set; }

        object IBaseUserRolesDto.UserId => UserId;

        object IBaseUserRolesDto.RoleId => RoleId;
    }
}