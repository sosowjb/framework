using Abp.Authorization;
using SOSOWJB.Framework.Authorization.Roles;
using SOSOWJB.Framework.Authorization.Users;

namespace SOSOWJB.Framework.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
