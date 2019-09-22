using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Engineer.Domain.Entities;

namespace Engineer.Application.Services.Mail
{
    public class MailService : IMailService
    {
        private readonly MailAddress _sender;
        private readonly SmtpClient _smtpClient;

        public MailService()
        {
            const string password = "N@vitas2018";
            _sender = new MailAddress("noreply.riaarhus@gmail.com", "Rådet for Ingeniørstuderende Aarhus");
            _smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_sender.Address, password)
            };
        }

        public async Task SendNewOrderMailAsync(string to, Order order)
        {
            var mail = new MailMessage(_sender, new MailAddress(to, to)) {Subject = "Udlånsordre"};

            var filePath = "C:\\Users\\Steiner\\Documents\\Github\\RiaarhusAPI\\Engineer.Application\\Services\\Mail\\Templates\\OrderMailTemplate.html"; 
            // ONLY USED IN DEV MODE - SEE ASP NET CORE SERVING STATIC FILES FROM A REQUEST PATH
               
            var fileStream = new StreamReader(filePath);
            string message = fileStream.ReadToEnd();
            fileStream.Close();

            message = message.Replace("[OrderName]", order.Name);
            message = message.Replace("[OrderNumber]", order.OrderNumber);

            mail.Body = message;
            mail.IsBodyHtml = true;

            await _smtpClient.SendMailAsync(mail);
        }


        public async Task SendMailAsync(string to, string subject, string message, string displayName = "Rådet for Ingeniørstuderende Aarhus")
        {
            using (var mail = new MailMessage(_sender, new MailAddress(to, to))
            {
                Subject = subject,
                Body = message
            })
            {
                mail.IsBodyHtml = true;
                await _smtpClient.SendMailAsync(mail);
            }
        }
    }
}