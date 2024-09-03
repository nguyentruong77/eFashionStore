using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace eFashionStore.MailService
{
    public class SendMailService
    {
        private readonly MailSettings _mailSettings;

        public SendMailService(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task<int> SendMail(MailContent mailContent)
        {
            var smtpClient = new SmtpClient(_mailSettings.Host)
            {
                Port = _mailSettings.Port,
                Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password),
                EnableSsl = _mailSettings.EnableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_mailSettings.Mail, _mailSettings.DisplayName),
                Subject = mailContent.Subject,
                Body = mailContent.Body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(mailContent.To);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
    }

    public class MailContent
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
