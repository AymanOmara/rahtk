﻿using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Rahtk.Contracts.Common;
using Rahtk.Shared.Localization;

namespace Rahtk.Infrastructure.EF.Services
{
	public class UserNotifier : IUserNotifier
	{
        private readonly IConfiguration _configuration;
        private readonly LanguageService _languageService;
        public UserNotifier(IConfiguration configuration, LanguageService languageService)
        {
            _configuration = configuration;
            _languageService = languageService;
        }


        public async Task Notify(string channel, string body)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Ra7tk", _configuration["Email:Email"]));
            mailMessage.To.Add(new MailboxAddress(channel, channel));
            mailMessage.Subject = _languageService.Getkey("email_verification").Value;
            mailMessage.Body = new TextPart()
            {
                Text = $"{_languageService.Getkey("your_email_verification_is").Value} {body}"
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.CheckCertificateRevocation = false;
                smtpClient.Connect("smtp.gmail.com", 587, false);
                smtpClient.Authenticate(_configuration["Email:Email"], _configuration["Email:Password"]);
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
                
            }
        }
    }
}

