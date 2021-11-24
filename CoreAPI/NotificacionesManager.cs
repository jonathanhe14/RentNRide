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

        public string generarModeloCorreo(Usuarios user, string titulo, string mensaje)
        {
            enviarCorreos(user, titulo, mensaje).Wait();
            return "éxito";
        }

        public static async Task enviarCorreos(Usuarios user, string titulo, string mensaje)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("rentnridecom@gmail.com", "RentNRide.com");
            var subject = titulo;
            var to = new EmailAddress(user.Correo, "Usuario");
            var plainTextContent = mensaje;
            var htmlContent = mensaje;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public string generarModeloSMS(Usuarios user, string mensaje)
        {
            enviarMensajes(user, mensaje);
            return "éxito";
        }

        private void enviarMensajes(Usuarios user, string mensaje)
        {
            string accountSid = "AC8cb56422437c955f6487c195eecccaa6";
            string authToken = "b402568cb8a6047c8725bb097c792f22";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: mensaje,
                from: new Twilio.Types.PhoneNumber("+12059531826"),
                to: new Twilio.Types.PhoneNumber("+506" + user.Telefono)
            );

            Console.WriteLine(message.Sid);
        }

        public string recuperarClaveCorreo(Usuarios user)
        {
            EnviarCorreoContrasenna(user).Wait();
            return "éxito";
        }

        public static async Task EnviarCorreoContrasenna(Usuarios user)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("rentnridecom@gmail.com", "RentNRide.com");
            var subject = "Resultado operación";
            var to = new EmailAddress(user.Correo, "Usuario");
            var plainTextContent = "El código OTP es el siguiente: ";
            var htmlContent = Convert.ToString(user.OTP);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public string recuperarClaveSMS(Usuarios user)
        {
            EnviarSMSContrasenna(user);
            return "éxito";
        }

        public static void EnviarSMSContrasenna(Usuarios user)
        {

            string accountSid = "AC8cb56422437c955f6487c195eecccaa6";
            string authToken = "b402568cb8a6047c8725bb097c792f22";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "El código OTP es el siguiente: " + user.OTP,
                from: new Twilio.Types.PhoneNumber("+12059531826"),
                to: new Twilio.Types.PhoneNumber("+506" + user.Telefono)
            );

            Console.WriteLine(message.Sid);
        }

    }
}
