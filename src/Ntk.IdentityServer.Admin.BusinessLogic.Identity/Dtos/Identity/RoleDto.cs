// Copyright (c) Jan Škoruba. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System.ComponentModel.DataAnnotations;
using Ntk.IdentityServer.Admin.BusinessLogic.Identity.Dtos.Identity.Base;
using Ntk.IdentityServer.Admin.BusinessLogic.Identity.Dtos.Identity.Interfaces;

namespace Ntk.IdentityServer.Admin.BusinessLogic.Identity.Dtos.Identity
{
    public class RoleDto<TKey> : BaseRoleDto<TKey>, IRoleDto
    {      
        [Required]
        public string Name { get; set; }
    }
}