namespace RandevuYonetimSistemi.Services
{
    using Microsoft.Extensions.Options;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        //Mail gönderme metodumuz 
        public void SendEmailAsync(string toEmail, string subject, string body)
        {
            //Mesajı hangi maile göndereceğimizi ve mesaj konusunu belirliyoruz
            var message = new MailMessage();
            message.From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName);
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = body;

            //Mesajı göndereceğimiz portu, hostu ve mesajın gönderileceği maili seçiyoruz
            
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new NetworkCredential
                (_emailSettings.SenderEmail, _emailSettings.SenderPassword);
            istemci.Port = _emailSettings.SmtpPort;
            istemci.Host = _emailSettings.SmtpServer;
            istemci.EnableSsl = true;

            istemci.Send(message);

        }

    }

}
