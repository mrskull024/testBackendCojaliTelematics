using Application.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Infraestructure.Services
{
    public class EmailRepository : IEmailService
    {
        public async Task<bool> SendEmail(string email, string subject, string message)
        {
            return true;
            //Este es un ejemplo de envio de notificacion por email, por lo que
            //este metodo siempre retornara true, ya que no se tiene un servidor de correo

            try
            {
                using var smtpClient = new SmtpClient("smtptest.com", 587)
                {
                    Credentials = new NetworkCredential("User", "Password"),
                    EnableSsl = true
                };

                using var mail = new MailMessage
                {
                    From = new MailAddress("smtpUser"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mail.To.Add(email);

                await smtpClient.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
