using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DotNETVueJSTemplate.Services.Email
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string email, string subject, string message, Attachment attachment = null)
        {
            this.Execute(new[] { email }, null, null, subject, message, attachment);
        }

        public Task SendEmailAsync(string email, string subject, string message, Attachment attachment = null)
        {
            return this.ExecuteAsync(new[] { email }, null, null, subject, message, attachment);
        }

        public void SendEmails(IList<string> to, IList<string> cc, IList<string> bcc, string subject,
            string message, Attachment attachment = null)
        {
            this.Execute(to, cc, bcc, subject, message, attachment);
        }

        public Task SendEmailsAsync(IList<string> to, IList<string> cc, IList<string> bcc, string subject,
            string message, Attachment attachment = null)
        {
            return this.ExecuteAsync(to, cc, bcc, subject, message, attachment);
        }

        private void Execute(IList<string> to, IList<string> cc, IList<string> bcc, string subject, string message, Attachment attachment = null)
        {
            using (var emailMessage = new MailMessage())
            {
                this.SetupEmailMessage(emailMessage, to, cc, bcc, subject, message, attachment);

                using (var client = new SmtpClient())
                {
                    client.Send(emailMessage);
                }
            }
        }

        private async Task ExecuteAsync(IList<string> to, IList<string> cc, IList<string> bcc, string subject, string message, Attachment attachment = null)
        {
            using (var emailMessage = new MailMessage())
            {
                this.SetupEmailMessage(emailMessage, to, cc, bcc, subject, message, attachment);

                using (var client = new SmtpClient())
                {
                    await client.SendMailAsync(emailMessage);
                }
            }
        }

        private void SetupEmailMessage(MailMessage emailMessage, IList<string> to, IList<string> cc, IList<string> bcc, string subject, string message, Attachment attachment = null)
        {
            if (to != null)
            {
                foreach (var email in to)
                {
                    emailMessage.To.Add(new MailAddress(email));
                }
            }

            if (cc != null)
            {
                foreach (var email in cc)
                {
                    emailMessage.CC.Add(new MailAddress(email));
                }
            }

            if (bcc != null)
            {
                foreach (var email in bcc)
                {
                    emailMessage.Bcc.Add(new MailAddress(email));
                }
            }

            emailMessage.Subject = subject;
            emailMessage.Body = message;
            emailMessage.IsBodyHtml = true;
            emailMessage.BodyEncoding = Encoding.UTF8;

            if (attachment != null)
            {
                emailMessage.Attachments.Add(attachment);
            }
        }
    }
}
