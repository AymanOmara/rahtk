using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Rahtk.Contracts.Common;

namespace Rahtk.Infrastructure.EF.Services
{
	public class UserNotifier :IUserNotifier
	{
        private readonly IConfiguration _configuration;

        public UserNotifier(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task Notify(string channel, string body)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Ayman", "aymantesting555@gmail.com"));
            mailMessage.To.Add(new MailboxAddress("to name", channel));
            mailMessage.Subject = "subject";
            mailMessage.Body = new TextPart("plain")
            {
                Text = "Hello"
            };

            using (var smtpClient = new SmtpClient(new ProtocolLogger("smtp.log")))
            {
                smtpClient.ServerCertificateValidationCallback = (object sender,
                X509Certificate certificate,
                X509Chain chain,
                SslPolicyErrors sslPolicyErrors) => true;
                //smtpClient.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Ssl2 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
                smtpClient.CheckCertificateRevocation = false;
                smtpClient.Connect("smtp.gmail.com", 587, false);
                smtpClient.Authenticate("aymantesting555@gmail.com", "@Ayman555testing");
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
                
            }

            //var mailMessage = new MailMessage
            //{
            //    From = new MailAddress("email"),
            //    Subject = "subject",
            //    Body = "<h1>Hello</h1>",
            //    IsBodyHtml = true,
            //};
            //mailMessage.To.Add("recipient");

            //smtpClient.Send(mailMessage);
            //var smtpClient = new SmtpClient("smtp.example.com")
            //{
            //    Port = 587,
            //    Credentials = new NetworkCredential("aymanomara55@gmail.com", "Ayman@2023"),
            //    EnableSsl = true,
            //};
            //var htmlContent = $"<strong>{body}</strong>";
            //smtpClient.Send("aymanomara55@gmail.com", channel, "subject", htmlContent);
            //var apiKey = _configuration["SendGrid:SendGridKey"];
            //var senderEmail = _configuration["SendGrid:SendGridEmail"];

            //var client = new SendGridClient(apiKey);
            //var from_email = new EmailAddress(senderEmail, "Example User");
            //var subject = "shift report";
            //var to_email = new EmailAddress(channel, "Example User");

            //var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, body, htmlContent);
            //var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

        }
    }
}

