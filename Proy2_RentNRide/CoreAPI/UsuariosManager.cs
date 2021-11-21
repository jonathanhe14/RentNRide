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

        public UsuariosManagement()
        {
            crudUsuarios = new UsuariosCrudFactory();
            crudContrasennas = new ContrasennaCrudFactory();
            crudRoles = new UsuariosRolCrudFactory();
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
                usuario.Estado = "Pendiente";
                usuario.OTP = 1;
                usuario.PersoneriaJuridica = "NULL";
                usuario.PermisoOperaciones = "NULL";
                usuario.Rol = 4;
                var contrasenna = new Contrasennas
                {
                    Contrasenna = usuario.ContrassenaActual,
                    Correo = usuario.Correo,
                    Fecha = DateTime.Now
                };

                var rolUsuario = new UsuariosRol
                {
                    IdUsuario = usuario.Correo,
                    IdRol = 4,
                    Estado = "Activo"

                };

                //Validacion del Rol y de los campos necesarios


                crudUsuarios.Create(usuario);
                crudContrasennas.Create(contrasenna);
                crudRoles.Create(rolUsuario);
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
                usuario.Estado = "Pendiente";
                usuario.OTP = 1;
                usuario.FechaNacimiento = DateTime.Now;
                usuario.Rol = 2;

                var contrasenna = new Contrasennas
                {
                    Contrasenna = usuario.ContrassenaActual,
                    Correo = usuario.Correo,
                    Fecha = DateTime.Now
                };
                var rolUsuario = new UsuariosRol
                {
                    IdUsuario = usuario.Correo,
                    IdRol = 2,
                    Estado = "Activo"

                };

                crudUsuarios.Create(usuario);
                crudContrasennas.Create(contrasenna);
                crudRoles.Create(rolUsuario);
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

    }
}
