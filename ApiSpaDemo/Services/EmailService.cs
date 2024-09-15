using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using ApiSpaDemo.Models;

namespace ApiSpaDemo.Services
{
    public class EmailService
    {
        private readonly SMTPSettingsModel _smtpSettingsModel;

        public EmailService(IOptions<SMTPSettingsModel> smtpSettingsModel)
        {
            _smtpSettingsModel = smtpSettingsModel.Value;
        }

        public async Task SendEmailAsync(string recipienteEmail, string asunto, string cuerpoMensaje)
        {
            var message = CrearMensajeMime(recipienteEmail, asunto);

            var bodyBuilder = new BodyBuilder { HtmlBody = "Su consulta es:\n" + cuerpoMensaje + "\nEn breve le contestaremos." };

            message.Body = bodyBuilder.ToMessageBody();

            await MandarEmail(message);
        }

        public async Task SendEmailCVAsync(string recipienteEmail, string asunto, string cuerpoMensaje, byte[] archivoAdjunto, string nombreArchivo)
        {
            var message = CrearMensajeMime(recipienteEmail, asunto);

            var bodyBuilder = new BodyBuilder { HtmlBody = cuerpoMensaje };

            // Agregar el archivo adjunto
            bodyBuilder.Attachments.Add(nombreArchivo, archivoAdjunto);

            message.Body = bodyBuilder.ToMessageBody();

            await MandarEmail(message);
        }

        private MimeMessage CrearMensajeMime(string recipienteEmail, string asunto)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettingsModel.SenderName, _smtpSettingsModel.SenderEmail));
            message.To.Add(new MailboxAddress("", recipienteEmail));
            message.Subject = asunto;
            return message;
        }

        private async Task MandarEmail(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettingsModel.Server, _smtpSettingsModel.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpSettingsModel.Username, _smtpSettingsModel.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}