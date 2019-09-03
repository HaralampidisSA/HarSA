using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HarSA.AspNetCore.Mvc.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public NotificationService(IHttpContextAccessor httpContextAccessor,
            ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        private void PrepareTempData(NotifyType notifyType, string message, bool encode = true)
        {
            var context = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(context);

            //Messages have stored in a serialized list
            var messages = tempData.ContainsKey(MessageDefaults.NotificationListKey)
                ? JsonConvert.DeserializeObject<IList<NotifyData>>(tempData[MessageDefaults.NotificationListKey].ToString())
                : new List<NotifyData>();

            messages.Add(new NotifyData
            {
                Message = message,
                NotifyType = notifyType,
                Encode = encode
            });

            tempData[MessageDefaults.NotificationListKey] = JsonConvert.SerializeObject(messages);
        }

        public void ErrorNotification(string message, bool encode = true)
        {
            PrepareTempData(NotifyType.Error, message, encode);
        }

        public void ErrorNotification(Exception exception, bool logException = true)
        {
            if (exception == null)
                return;

            ErrorNotification(exception.Message);
        }

        public void Notification(NotifyType type, string message, bool encode = true)
        {
            PrepareTempData(type, message, encode);
        }

        public void SuccessNotification(string message, bool encode = true)
        {
            PrepareTempData(NotifyType.Success, message, encode);
        }

        public void WarningNotification(string message, bool encode = true)
        {
            PrepareTempData(NotifyType.Warning, message, encode);
        }
    }
}
