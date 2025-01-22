using Aplicacion.Infraestructura.EnviarEmail.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Infraestructura.EnviarEmail.Implementaciones
{
    public class EnviarEmailConGmail : IEnviarEmail
    {
        private readonly SmtpClient smtpClient;
        private readonly string email;
        public EnviarEmailConGmail()
        {
            email = Environment.GetEnvironmentVariable("EMAIL_USER")!;
            smtpClient = new SmtpClient { 
            EnableSsl = true,
            UseDefaultCredentials = false,
            Host = "smtp.gmail.com",
            Port = 587,
            Credentials = new NetworkCredential
            {
                UserName = this.email,
                Password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
            }
            };
        }
        public async Task Ejecutar(string destinatario, string asunto, string cuerpo)
        {
            MailMessage mailMessage = new(this.email, destinatario, asunto, cuerpo);
            mailMessage.IsBodyHtml = true;
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
