using System.Collections.Generic;
using SOSOWJB.Framework.Authorization.Permissions.Dto;

namespace SOSOWJB.Framework.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}