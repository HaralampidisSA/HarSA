using System;

namespace HarSA.AspNetCore.Mvc.Notifications
{
    public interface INotificationService
    {
        void Notification(NotifyType type, string message, bool encode = true);

        void SuccessNotification(string message, bool encode = true);

        void WarningNotification(string message, bool encode = true);

        void ErrorNotification(string message, bool encode = true);

        void ErrorNotification(Exception exception, bool logException = true);
    }
}
