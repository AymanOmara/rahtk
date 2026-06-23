using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;
using Rahtk.Contracts.Common;

namespace Rahtk.Infrastructure.EF.Services
{
    public class NotificationSender(ILogger<NotificationSender> logger) : INotificationSender
    {
        public async Task SendNotification(string deviceToken, string messageBody)
        {
            try
            {
                var firebaseMessage = new Message()
                {
                    Notification = new Notification
                    {
                        Title = "Rahtk",
                        Body = messageBody,
                    },
                    Token = deviceToken,
                };
                var messaging = FirebaseMessaging.DefaultInstance;
                var result = await messaging.SendAsync(firebaseMessage);
                logger.LogInformation("Firebase notification sent successfully. MessageId: {MessageId}", result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send Firebase notification to device token: {Token}", deviceToken);
            }
        }
    }
}