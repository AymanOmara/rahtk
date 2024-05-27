﻿using FirebaseAdmin.Messaging;
using Rahtk.Contracts.Common;

namespace Rahtk.Infrastructure.EF.Services
{
    public class NotificationSender : INotificationSender
    {
        public NotificationSender()
        {
        }

        public async Task SendNotification(string deviceToken, string messageBody)
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = "Message Title",
                    Body = messageBody,
                },
                Token = deviceToken,
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
        }
    }
}