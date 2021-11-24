using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Crud;
using Entities_POJO;
using Exceptions;

namespace CoreAPI
{
    public class UsuariosManagement : BaseEntity
    {
        private UsuariosCrudFactory crudUsuarios;
        private ContrasennaCrudFactory crudContrasennas;
        private UsuariosRolCrudFactory crudRoles;
        private UserProfileCrudFactory crudUserProfile;
        private Hasher encriptado;

        public UsuariosManagement()
        {
            crudUsuarios = new UsuariosCrudFactory();
            crudContrasennas = new ContrasennaCrudFactory();
            crudRoles = new UsuariosRolCrudFactory();
            crudUserProfile = new UserProfileCrudFactory();
            encriptado = new Hasher();
        }

        public void CreateUsuario(Usuarios usuario)
        {
            try
            {

                //Valida si ya existe
                var u = crudUsuarios.VerificarUsuario<Usuarios>(usuario);
                if (u != null)
                {
                    throw new BussinessException(1);
                }
                //Valida si es mayor de edad
                if (usuario.Edad < 18)
                {
                    throw new BussinessException(2);

                }
                usuario.Comprobacion = "NULL";
                usuario.Estado = "PENDIENTE";
                usuario.OTP = 0;
                usuario.PersoneriaJuridica = "NULL";
                usuario.PermisoOperaciones = "NULL";
                usuario.Rol = 4;
                var contrasenna = new Contrasennas
                {
                    Contrasenna = encriptado.MD5(usuario.ContrassenaActual),
                    Correo = usuario.Correo,
                    Fecha = DateTime.Now
                };

                var rolUsuario = new UsuariosRol
                {
                    IdUsuario = usuario.Correo,
                    IdRol = 4,
                    Estado = "Activo"

                };
                var userProfile = new UserProfile
                {
                    UserName = usuario.Correo,
                    Password = encriptado.MD5(usuario.ContrassenaActual),
                    FullName = usuario.Nombre + usuario.Apellidos
                };

                //Validacion del Rol y de los campos necesarios


                crudUsuarios.Create(usuario);
                crudContrasennas.Create(contrasenna);
                crudRoles.Create(rolUsuario);
                crudUserProfile.Create(userProfile);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }


        public void CreateSocio(Usuarios usuario)
        {
            try
            {

                //Valida si ya existe
                var u = crudUsuarios.Retrieve<Usuarios>(usuario);
                usuario.Apellidos = "NUll";
                usuario.Comprobacion = "NULL";
                usuario.Edad = 0;
                usuario.Estado = "PENDIENTE";
                usuario.OTP = 0;
                usuario.FechaNacimiento = DateTime.Now;
                usuario.Rol = 2;
                usuario.OTPSMS = 0; 
                var contrasenna = new Contrasennas
                {
                    Contrasenna = encriptado.MD5(usuario.ContrassenaActual),
                    Correo = usuario.Correo,
                    Fecha = DateTime.Now
                };
                var rolUsuario = new UsuariosRol
                {
                    IdUsuario = usuario.Correo,
                    IdRol = 2,
                    Estado = "Activo"

                };
                var userProfile = new UserProfile
                {
                    UserName = usuario.Correo,
                    Password = encriptado.MD5(usuario.ContrassenaActual),
                    FullName = usuario.Nombre + usuario.Apellidos
                };

                crudUsuarios.Create(usuario);
                crudContrasennas.Create(contrasenna);
                crudRoles.Create(rolUsuario);
                crudUserProfile.Create(userProfile);

            }
            catch (Exception ex)
            {

                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Usuarios> RetrieveAll()
        {
            try
            {
                return crudUsuarios.RetrieveAll<Usuarios>();
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }

        }

        public Usuarios TraerUsuario(Usuarios usuarios)
        {
            try
            {
                return crudUsuarios.VerificarUsuario<Usuarios>(usuarios);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }

        public Usuarios RetrieveById(Usuarios usuarios)
        {
            try
            {
                return crudUsuarios.Retrieve<Usuarios>(usuarios);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }

        public void Update(Usuarios usuarios)
        {
            try
            {
                crudUsuarios.Update(usuarios);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }
        public void CreateClave(Contrasennas clave)
        {
            crudContrasennas.Create(clave);
        }

        public List<Contrasennas> RetrieveClavesById(Contrasennas clave)
        {
            return crudContrasennas.RetrieveTodo<Contrasennas>(clave);
        }


        public Usuarios AsignarOTP(Usuarios user)
        {

            Random generator = new Random();
            user.OTP = generator.Next(100000, 1000000);

            return user;
        }
        public Usuarios AsignarOTPSMS(Usuarios user)
        {

            Random generator = new Random();
            user.OTPSMS = generator.Next(100000, 1000000);

            return user;
        }

        public Usuarios EvioOTPCorreo(Usuarios user)
        {
            Usuarios u = null;
            var mngUsuarios = new UsuariosManagement();
            u = mngUsuarios.RetrieveById(user);
            if (u != null)
            {
                u = AsignarOTP(u);
                mngUsuarios.Update(u);
                //var mngNotificaciones = new NotificacionesManager();
                //mngNotificaciones.recuperarClaveCorreo(u);
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

        public Usuarios EnvioOTPSMS(Usuarios user)
        {
            Usuarios u = null;
            var mngUsuarios = new UsuariosManagement();
            u = mngUsuarios.RetrieveById(user);
            if (u != null)
            {
                u = AsignarOTPSMS(u);
                mngUsuarios.Update(u);
                //var mngNotificaciones = new NotificacionesManager();
                //mngNotificaciones.recuperarClaveSMS(u);
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

        public string validarOTP(Usuarios user)
        {
            try
            {
                Usuarios u = null;
                var mngUsuarios = new UsuariosManagement();
                u = mngUsuarios.RetrieveById(user);

                if (u.OTP == user.OTP && u.OTPSMS == user.OTPSMS)
                {
                    u.Estado = "VERIFICADO";
                    mngUsuarios.Update(u);
                    return "success";
                }
                else
                {
                    throw new BussinessException(0);
                }
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }
        public List<Usuarios> RetrieveAllSolicitudes() {
            try {
                return crudUsuarios.RetrieveAllSolicitudes<Usuarios>();
            } catch(Exception ex) {
                throw new BussinessException(0);
            }

        }
    }
}
