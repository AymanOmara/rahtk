using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Rahtk.Contracts.Common;
using Rahtk.Shared.Localization;

namespace Rahtk.Infrastructure.EF.Services
{
	public class UserNotifier(IConfiguration configuration, LanguageService languageService) : IUserNotifier
	{
        public async Task Notify(string channel, string body)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Ra7tk", configuration["Email:Email"]));
            mailMessage.To.Add(new MailboxAddress(channel, channel));
            mailMessage.Subject = languageService.GetKey("email_verification").Value;
            mailMessage.Body = new TextPart()
            {
                Text = $"{languageService.GetKey("your_email_verification_is").Value} {body}"
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.CheckCertificateRevocation = false;
                var host = configuration["Email:Host"] ?? "smtp.gmail.com";
                var port = int.TryParse(configuration["Email:Port"], out var p) ? p : 587;
                await smtpClient.ConnectAsync(host, port, false);
                await smtpClient.AuthenticateAsync(configuration["Email:Email"], configuration["Email:Password"]);
                await smtpClient.SendAsync(mailMessage);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}

