using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient
            {
                Credentials = new NetworkCredential($"{ConfigurationManager.AppSettings["MAIL_USER"]}", 
                                                    $"{ConfigurationManager.AppSettings["MAIL_PASS"]}"),
                EnableSsl = true,
                Port = 2525,
                Host = "sandbox.smtp.mailtrap.io"
            };
        }

        public void CreateMail(string sourceEmail, string destinationEmail, string subject, string body)
        {
            email = new MailMessage(sourceEmail, destinationEmail);
            email.Subject = subject;
            email.IsBodyHtml = true;
            email.Body = body;
        }

        public void SendMail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
