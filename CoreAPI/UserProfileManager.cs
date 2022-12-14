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
            UserProfile u = new UserProfile();
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

        public void UpdateUser(UserProfile user)
        {
            crudUser.Update(user);
        }

        public Usuarios recuperarClaveCorreo(Usuarios user)
        {
            Usuarios u = null;
            var mngUsuarios = new UsuariosManagement();
            u = mngUsuarios.RetrieveById(user);
            if (u != null)
            {
                u = AsignarOTP(u);
                mngUsuarios.Update(u);                
                var mngNotificaciones = new NotificacionesManager();
                mngNotificaciones.recuperarClaveCorreo(u);
                Usuarios usuario = new Usuarios
                {
                    Correo = u.Correo,
                    Telefono = u.Telefono
                };
                return usuario;
            }
            else
            {
                return u;
            }
        }

        public Usuarios recuperarClaveTelefono(Usuarios user)
        {
            Usuarios u = null;
            var mngUsuarios = new UsuariosManagement();
            u = mngUsuarios.RetrieveById(user);
            if (u != null)
            {
                u = AsignarOTP(u);
                mngUsuarios.Update(u);
                var mngNotificaciones = new NotificacionesManager();
                mngNotificaciones.recuperarClaveSMS(u);
                Usuarios usuario = new Usuarios
                {
                    Correo = u.Correo,
                    Telefono = u.Telefono
                };
                return usuario;
            }
            else
            {
                return u;
            }
        }

        public Usuarios AsignarOTP(Usuarios user)
        {

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
                return "success";
            }
            else
            {
                return "El OTP no coincide con el OTP enviado";
            }
        }

        public string actualizarClave(Usuarios user)
        {
            Usuarios u = null;
            var mngUsuarios = new UsuariosManagement();
            var fortalezaClave = new FortalezaClave();
            u = mngUsuarios.RetrieveById(user);
            Contrasennas clave = new Contrasennas
            {
                Fecha = DateTime.Now,
                Contrasenna = user.ContrassenaActual,
                Correo = user.Correo
            };

            if (u != null)
            {

                switch (fortalezaClave.GetPasswordStrength(clave.Contrasenna))
                {
                    case FortalezaClave.PasswordStrength.Blanco:
                        return "La contraseña está en blanco o no cumple los requerimientos de seguridad";
                    case FortalezaClave.PasswordStrength.MuyDebil: case FortalezaClave.PasswordStrength.Debil:
                        case FortalezaClave.PasswordStrength.Media:
                        return "La contraseña no cumple los requerimientos de seguridad";
                    case FortalezaClave.PasswordStrength.Fuerte: case FortalezaClave.PasswordStrength.MuyFuerte:
                        Hasher encriptado = new Hasher();
                        clave.Contrasenna = encriptado.MD5(clave.Contrasenna);
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
                            UserProfile usuarioActualizado = new UserProfile();
                            usuarioActualizado.UserName = u.Correo;
                            usuarioActualizado.Password = clave.Contrasenna;
                            UpdateUser(usuarioActualizado);
                            var mngNotificaciones = new NotificacionesManager();
                            string titulo = "Cambio contraseña";
                            string mensaje = "Su contraseña ha sido cambiada éxitosamente";
                            mngNotificaciones.generarModeloCorreo(u, titulo, mensaje);
                            mngNotificaciones.generarModeloSMS(u, mensaje);
                            return "Su contraseña ha sido cambiada éxitosamente";
                        }
                        else
                        {
                            return "La contraseña no puede ser igual a las anteriores";
                        }                       
                    default:
                        return "Contraseña no clasificada";                        
                }              

            }
            else
            {
                return "Ocurrió un error";
            }


        }

    }
}
