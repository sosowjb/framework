using System.Threading.Tasks;
using Abp;
using Abp.Notifications;
using SOSOWJB.Framework.Authorization.Users;
using SOSOWJB.Framework.MultiTenancy;

namespace SOSOWJB.Framework.Notifications
{
    public interface IAppNotifier
    {
        Task WelcomeToTheApplicationAsync(User user);

        Task NewUserRegisteredAsync(User user);

        Task NewTenantRegisteredAsync(Tenant tenant);

        Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info);
    }
}
