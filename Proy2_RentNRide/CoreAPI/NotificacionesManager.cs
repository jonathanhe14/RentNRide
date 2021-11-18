using Entities_POJO;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CoreAPI
{
    public class NotificacionesManager
    {
        public NotificacionesManager()
        {

        }

        public string recuperarClaveCorreo(Usuarios user)
        {
            EnviarCorreoContrasenna(user).Wait();
            return "éxito";
        }

        public string generarModeloCorreo(Usuarios user, string titulo, string mensaje)
        {
            enviarCorreos(user, titulo, mensaje).Wait();
            return "éxito";
        }

        public static async Task enviarCorreos(Usuarios user, string titulo, string mensaje)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("astellerm@ucenfotec.ac.cr", "RentNRide.com");
            var subject = titulo;
            var to = new EmailAddress(user.Correo, "Usuario");
            var plainTextContent = mensaje;
            var htmlContent = mensaje;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public string recuperarClaveSMS(Usuarios user)
        {
            EnviarSMSContrasenna(user);
            return "éxito";
        }

        public static async Task EnviarCorreoContrasenna(Usuarios user)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("astellerm@ucenfotec.ac.cr", "RentNRide.com");
            var subject = "Resultado operación";
            var to = new EmailAddress(user.Correo, "Usuario");
            var plainTextContent = "El código OTP es el siguiente: ";
            var htmlContent = Convert.ToString(user.OTP);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public static void EnviarSMSContrasenna(Usuarios user)
        {

            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "El código OTP es el siguiente: " + user.OTP,
                from: new Twilio.Types.PhoneNumber("+14158914367"),
                to: new Twilio.Types.PhoneNumber("+506" + user.Telefono)
            );

            Console.WriteLine(message.Sid);
        }

    }
}
