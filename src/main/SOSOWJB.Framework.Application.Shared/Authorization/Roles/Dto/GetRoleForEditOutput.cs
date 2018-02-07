using System.Collections.Generic;
using SOSOWJB.Framework.Authorization.Permissions.Dto;

namespace SOSOWJB.Framework.Authorization.Roles.Dto
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}