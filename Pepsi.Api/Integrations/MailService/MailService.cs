using Coupons.PepsiKSA.Api.Core.ModelDto;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace Coupons.PepsiKSA.Api.Integrations.MailService
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Send(EmailProperties email)
        {
            var from = string.IsNullOrWhiteSpace(email.From) ? _configuration.GetSection("MailService:EmailFrom").Value : email.From;
            var fromDisplayName = string.IsNullOrWhiteSpace(email.FromDisplayName) ? _configuration.GetSection("MailService:EmailFromDisplayName").Value : email.FromDisplayName;
            var password = string.IsNullOrWhiteSpace(email.Password) ? _configuration.GetSection("MailService:EmailPassword").Value : email.Password;

            MailMessage mailMsg = new MailMessage
            {
                From = new MailAddress(from, fromDisplayName),
                IsBodyHtml = true,
                Subject = email.Subject,
                Body = email.Body,

            };

            // Init SmtpClient and send
            var smtpClient = new SmtpClient
            {
                Host = _configuration.GetSection("MailService:EmailHost").Value,
                Port = int.Parse(_configuration.GetSection("MailService:EmailPort").Value),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_configuration.GetSection("MailService:EmailFrom").Value, password),

                EnableSsl = true,

            };



            foreach (var address in email.To.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                mailMsg.To.Clear();
                mailMsg.To.Add(new MailAddress(address));
                smtpClient.Send(mailMsg);
            }
        }
    }
}
