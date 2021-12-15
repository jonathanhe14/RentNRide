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
        private MembresiaCrudFactory crudMembresiaUsuario;
        private MonederoCrudFactory crudMonedero;
        private Hasher encriptado;

        public UsuariosManagement()
        {
            crudUsuarios = new UsuariosCrudFactory();
            crudContrasennas = new ContrasennaCrudFactory();
            crudRoles = new UsuariosRolCrudFactory();
            crudUserProfile = new UserProfileCrudFactory();
            crudMembresiaUsuario = new MembresiaCrudFactory();
            crudMonedero = new MonederoCrudFactory();
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
                usuario.Comprobacion = "FALSE";
                usuario.Estado = "NULL";
                usuario.OTP = 0;
                usuario.PersoneriaJuridica = "NULL";
                usuario.PermisoOperaciones = "NULL";
                usuario.OTPSMS = 0;
                usuario.OTPVencimiento = DateTime.Now;
                var contrasenna = new Contrasennas
                {
                    Contrasenna = encriptado.MD5(usuario.ContrassenaActual),
                    Correo = usuario.Correo,
                    Fecha = DateTime.Now
                };

                var rolUsuario = new UsuariosRol
                {
                    IdUsuario = usuario.Correo,
                    IdRol = usuario.Rol,
                    Estado = "Activo"

                };
                var userProfile = new UserProfile
                {
                    UserName = usuario.Correo,
                    Password = encriptado.MD5(usuario.ContrassenaActual),
                    FullName = usuario.Nombre + usuario.Apellidos
                };
                var monedero = new Monedero
                {
                    IdUsuario = usuario.Correo,
                    Saldo = 0,
                    FechaCorte = DateTime.Now.AddMonths(1),
                    FechaExpiracion = DateTime.Now.AddDays(15),
                    InfoMonedero = "Usuario"

                };


                //Validacion del Rol y de los campos necesarios


                crudUsuarios.Create(usuario);
                crudContrasennas.Create(contrasenna);
                crudRoles.Create(rolUsuario);
                crudUserProfile.Create(userProfile);
                CreateMonedero(monedero);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public object UsuariosPerfil(Usuarios user)
        {
            Usuarios u = null;
            try
            {
                u = crudUsuarios.PerfilUsuario<Usuarios>(user);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return u;
        }

        public void CreateSocio(Usuarios usuario)
        {
            try
            {
                var u = crudUsuarios.VerificarUsuario<Usuarios>(usuario);
                if (u != null)
                {
                    throw new BussinessException(1);
                }


                usuario.Comprobacion = "FALSE";
                usuario.Estado = "NULL";
                usuario.OTP = 0;
                usuario.OTPSMS = 0;
                usuario.OTPVencimiento = DateTime.Now;
                if (usuario.Rol == 2)
                {
                    int edad = 0;
                    usuario.PermisoOperaciones = "NULL";
                    usuario.PersoneriaJuridica = "NULL";
                    if (usuario.FechaNacimiento.Year < DateTime.Now.Year)
                    {
                        edad = DateTime.Now.Year - usuario.FechaNacimiento.Year;
                        if (usuario.FechaNacimiento.Month > DateTime.Now.Month)
                        {
                            edad--;

                        }
                        else if (usuario.FechaNacimiento.Month == DateTime.Now.Month)
                        {
                            if (usuario.FechaNacimiento.Day > DateTime.Now.Day)
                            {
                                edad--;
                            }
                        }
                    }
                    usuario.Edad = edad;
                    if (usuario.Edad < 18)
                    {
                        throw new BussinessException(2);

                    }
                }
                else
                {

                    usuario.Apellidos = "NUll";
                    usuario.Edad = 0;
                    usuario.FechaNacimiento = DateTime.Now;
                }

                var contrasenna = new Contrasennas
                {
                    Contrasenna = encriptado.MD5(usuario.ContrassenaActual),
                    Correo = usuario.Correo,
                    Fecha = DateTime.Now
                };
                var rolUsuario = new UsuariosRol
                {
                    IdUsuario = usuario.Correo,
                    IdRol = usuario.Rol,
                    Estado = "Activo"

                };
                var userProfile = new UserProfile
                {
                    UserName = usuario.Correo,
                    Password = encriptado.MD5(usuario.ContrassenaActual),
                    FullName = usuario.Nombre + usuario.Apellidos
                };
                var monedero = new Monedero
                {
                    IdUsuario = usuario.Correo,
                    Saldo = 0,
                    FechaCorte = DateTime.Now.AddMonths(1),
                    FechaExpiracion = DateTime.Now.AddDays(15),
                    InfoMonedero = "Socio"

                };

                crudUsuarios.Create(usuario);
                crudContrasennas.Create(contrasenna);
                crudRoles.Create(rolUsuario);
                crudUserProfile.Create(userProfile);
                CreateMonedero(monedero);
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


        public Membresias TraerMembresiaUsuario(Membresias membresia)
        {
            try
            {
                return crudMembresiaUsuario.RetrieveMembresiaUsuario<Membresias>(membresia);
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

        public void CreateMonedero(Monedero monedero)
        {
            try
            {
                crudMonedero.Create(monedero);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }
        public Monedero RetrieveMonedero(string correo)
        {
            try
            {
                var monedero = new Monedero
                {
                    IdUsuario = correo
                };
                return crudMonedero.Retrieve<Monedero>(monedero);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }

        public void PutMonedero(Monedero monedero)
        {
            try
            {

                crudMonedero.Update(monedero);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }
        public void UpdateComprobanteMembresia(MembresiasUsuario MembresiaUsuario)
        {
            try
            {
                crudMembresiaUsuario.UpdateMembresiaUsuario(MembresiaUsuario);
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
            user.Telefono = "0";
            u = mngUsuarios.RetrieveById(user);
            if (u != null)
            {
                u = AsignarOTP(u);
                u.OTPVencimiento = DateTime.Now;
                mngUsuarios.Update(u);
                var mngNotificaciones = new NotificacionesManager();
                mngNotificaciones.recuperarClaveCorreo(u);
                Usuarios usuario = new Usuarios
                {
                    Correo = u.Correo,
                    Telefono = "0"
                };
                return usuario;
            }
            else
            {
                return u;
            }
        }

        public List<Usuarios> Perfil(Usuarios user)
        {

            try
            {

                return crudUsuarios.TraerPerfil<Usuarios>(user);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }


        }


        public Usuarios EnvioOTPSMS(Usuarios user)
        {
            Usuarios u = null;
            var mngUsuarios = new UsuariosManagement();
            user.Telefono = "0";
            u = mngUsuarios.RetrieveById(user);
            if (u != null)
            {
                u = AsignarOTPSMS(u);
                mngUsuarios.Update(u);
                u.OTPVencimiento = DateTime.Now;
                var mngNotificaciones = new NotificacionesManager();
                //mngNotificaciones.recuperarClaveSMS2(u);
                Usuarios usuario = new Usuarios
                {
                    Correo = u.Correo,
                    Telefono = "0"
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
                user.Telefono = "0";
                u = mngUsuarios.RetrieveById(user);

                if (u.OTP == user.OTP && u.OTPSMS == user.OTPSMS)
                {
                    if (u.OTPVencimiento.Year == DateTime.Now.Year && u.OTPVencimiento.Month == DateTime.Now.Month &&
                        u.OTPVencimiento.Day == DateTime.Now.Day)
                    {
                        if ((DateTime.Now.Minute - u.OTPVencimiento.Minute) <= 15)
                        {
                            u.Comprobacion = "TRUE";
                            u.Estado = "PENDIENTE";
                            mngUsuarios.Update(u);
                            return "success";
                        }
                    }
                    throw new BussinessException(0);
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
        public List<Usuarios> RetrieveAllSolicitudes()
        {
            try
            {
                return crudUsuarios.RetrieveAllSolicitudes<Usuarios>();
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }

        }
    }
}
