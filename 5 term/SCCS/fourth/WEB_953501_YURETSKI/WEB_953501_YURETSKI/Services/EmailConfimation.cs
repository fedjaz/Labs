using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace WEB_953501_YURETSKI.Services
{
    public class EmailConfimation
    {
        public string CredentialEmail {  get; set; }
        public string CredentialPassword {  get; set; }

        public NetworkCredential networkCredential {  get; set; }
        public EmailConfimation(string credentialEmail, string credentialPassword)
        {
            CredentialEmail = credentialEmail;
            CredentialPassword = credentialPassword;
            networkCredential = new NetworkCredential(credentialEmail, credentialPassword);
        }

        public string BeginConfirmation(string email)
        {
            string code = GenerateCode(32);
            MailAddress from = new MailAddress(CredentialEmail);
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Подтверждение email";
            StringBuilder body = new StringBuilder();
            body.Append("<h2>Подтверждение email для WEB_953501_YURETSKI.");
            body.Append("<br/>");
            body.AppendLine($"<a href=\"https://localhost:44380/Account/Confirm?email={email}&code={code}\">Нажмите, чтобы подтвердить email</a>");
            message.Body = body.ToString();
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = networkCredential;
            smtpClient.Send(message);

            return code;
        }

        private string GenerateCode(int length)
        {
            StringBuilder buffer = new StringBuilder();
            Random random = new Random();
            for(int i  = 0; i < length; i++)
            {
                buffer.Append((char)random.Next('A', 'Z' + 1));
            }
            return buffer.ToString();
        }
    }
}
