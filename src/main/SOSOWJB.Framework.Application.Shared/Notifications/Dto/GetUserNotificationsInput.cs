using Abp.Notifications;
using SOSOWJB.Framework.Dto;

namespace SOSOWJB.Framework.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}