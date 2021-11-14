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

        public string recuperarClaveCorreo(UserProfile user)
        {
            EnviarCorreoContrasenna(user).Wait();
            return "éxito";
        }

        public string recuperarClaveSMS(UserProfile user)
        {
            EnviarSMSContrasenna(user);
            return "éxito";
        }

        public static async Task EnviarCorreoContrasenna(UserProfile user)
        {

            /*
             * Recibe desde antes el usuario (pues se supone que ya existe entonces no es necesario buscarlo).
             * Le asigna el otp en user profile manager
             * RECORDAR CAMBIAR CORREOS Y DEMÁS
             */
            var mng = new UserProfileManager();
            int codigoOTP = mng.AsignarOTP(user);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("astellerm@ucenfotec.ac.cr", "RentNRide.com");
            var subject = "Resultado operación";
            var to = new EmailAddress("astellerm@gmail.com", "Usuario");
            var plainTextContent = "El código OTP es el siguiente: ";
            var htmlContent = Convert.ToString(codigoOTP);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public static void EnviarSMSContrasenna(UserProfile user)
        {

            var mng = new UserProfileManager();
            int codigoOTP = mng.AsignarOTP(user);

            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "El código OTP es el siguiente: " + codigoOTP,
                from: new Twilio.Types.PhoneNumber("+14158914367"),
                to: new Twilio.Types.PhoneNumber("+506" + "89354513")
            );

            Console.WriteLine(message.Sid);
        }

    }
}
