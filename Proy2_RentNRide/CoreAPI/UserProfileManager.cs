using DataAccess.Crud;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class UserProfileManager
    {
        private UserProfileCrudFactory crudUser;
        private ContrasennaCrudFactory crudContrasennas;


        public UserProfileManager()
        {
            crudUser = new UserProfileCrudFactory();
            crudContrasennas = new ContrasennaCrudFactory();
        }


        public UserProfile ValidateUser(UserProfile user)
        {
            UserProfile u = null;
            u = crudUser.Retrieve<UserProfile>(user);
            /*try
            {

                u = crudUser.Retrieve<UserProfile>(user);
                if (u == null)
                {
                    throw new BussinessException(9);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }*/

            return u;
        }

        public string recuperarClaveCorreo(Usuarios user)
        {
            Usuarios u = null;
            var mngUsuarios = new UsuariosManagement();
            u = mngUsuarios.RetrieveById(user);
            if (u != null)
            {
                u = AsignarOTP(u);
                //u.Comprobacion = "true";
                mngUsuarios.Update(u);
                var mngNotificaciones = new NotificacionesManager();
                mngNotificaciones.recuperarClaveCorreo(u);
                return "éxito";
            }
            else
            {
                return "fracaso";
            }
        }

        public Usuarios AsignarOTP(Usuarios user)
        {
            /*
             * Recibe de las notificaciones el usuario.
             * Asigna un OTP:
             * usuario.otp = codigoOTP;(POR EJEMPLO).
             * Finalmente, lo devuelve a las notificaciones.
             * RECORDAR CAMBIAR DE INT A USUARIO EL TIPO DE FUNCIÓN
             */
            Random generator = new Random();
            user.OTP = generator.Next(100000, 1000000);

            return user;
        }

        public string validarOTP(Usuarios user)
        {
            Usuarios u = null;
            var mngUsuarios = new UsuariosManagement();
            u = mngUsuarios.RetrieveById(user);
            
            if (u.OTP == user.OTP)
            {
                return "éxito";
            }
            else
            {
                return "fracaso";
            }
        }

        public string actualizarClave(Usuarios user)
        {
            Usuarios u = null;
            var mngUsuarios = new UsuariosManagement();
            u = mngUsuarios.RetrieveById(user);
            Contrasennas clave = new Contrasennas
            {
                Fecha = DateTime.Now,
                Contrasenna = user.ContrassenaActual,
                Correo = user.Correo
            };

            if (u != null)
            {
                List<Contrasennas> historicoClaves = mngUsuarios.RetrieveClavesById(clave);
                Boolean prueba = false;
                foreach (var contrasenna in historicoClaves)
                {
                    if (contrasenna.Contrasenna.Equals(clave.Contrasenna))
                    {
                        prueba = true;
                        break;
                    }
                }

                if (!prueba)
                {
                    u.ContrassenaActual = user.ContrassenaActual;
                    mngUsuarios.Update(u);
                    mngUsuarios.CreateClave(clave);
                    var mngNotificaciones = new NotificacionesManager();
                    string titulo = "Cambio contraseña";
                    string mensaje = "Su contraseña ha sido cambiada éxitosamente";
                    //mngNotificaciones.generarModeloCorreo(u, titulo, mensaje);
                    return "Su contraseña ha sido cambiada éxitosamente";
                }
                else
                {
                    return "La contraseña no puede ser igual a las anteriores";
                }

            }
            else
            {
                return "Ocurrió un error";
            }


        }

    }
}
