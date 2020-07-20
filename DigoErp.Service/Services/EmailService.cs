using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DigoErp.Service.Services
{
    public class EmailService : BaseService
    {
        private readonly string FromEmail;
        private readonly string FromDisplayName;
        private readonly string SmtpServer;
        private readonly string FromPassword;
        private readonly string Port;
        private SmtpClient smtpClient;
        public EmailService()
        {
            SmtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            Port = ConfigurationManager.AppSettings["Port"];
            FromEmail = ConfigurationManager.AppSettings["FromEmail"];
            FromPassword = ConfigurationManager.AppSettings["FromPassword"];
            FromDisplayName = "DigoSolutions";
        }
        public Task SendAsync(IdentityMessage message, string storeId = "")
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(FromEmail, FromDisplayName)
            };
            mailMessage.To.Add(new MailAddress(message.Destination));
            mailMessage.Subject = message.Subject;
            mailMessage.BodyEncoding = Encoding.UTF8;
            if (!string.IsNullOrEmpty(message.Body))
            {
                mailMessage.Body = message.Body;
            }
            smtpClient = SetSmtpClient();
            mailMessage.HeadersEncoding = Encoding.Default;
            mailMessage.IsBodyHtml = true;
            smtpClient.Send(mailMessage);
            return Task.FromResult(0);
        }

        private SmtpClient SetSmtpClient()
        {
            var portInt = Convert.ToInt32(Port);
            var client = new SmtpClient(SmtpServer, portInt)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(FromEmail, FromPassword)
            };
            return client;
        }
    }
}