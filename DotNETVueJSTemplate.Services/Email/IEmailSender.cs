using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DotNETVueJSTemplate.Services.Email
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, string message, Attachment attachment = null);

        Task SendEmailAsync(string email, string subject, string message, Attachment attachment = null);

        void SendEmails(IList<string> to, IList<string> cc, IList<string> bcc, string subject, string message, Attachment attachment = null);

        Task SendEmailsAsync(IList<string> to, IList<string> cc, IList<string> bcc, string subject, string message, Attachment attachment = null);
    }
}
