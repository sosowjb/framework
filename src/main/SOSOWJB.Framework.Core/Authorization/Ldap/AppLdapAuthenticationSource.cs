#if FEATURE_LDAP
using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using SOSOWJB.Framework.Authorization.Users;
using SOSOWJB.Framework.MultiTenancy;

namespace SOSOWJB.Framework.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}
#endif