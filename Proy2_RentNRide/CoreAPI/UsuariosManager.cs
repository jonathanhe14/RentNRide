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

        public UsuariosManagement()
        {
            crudUsuarios = new UsuariosCrudFactory();
            crudContrasennas = new ContrasennaCrudFactory();
        }

        public void Create(Usuarios usuario)
        {
            try
            {

                //Valida si ya existe
                var u = crudUsuarios.Retrieve<Usuarios>(usuario);
                if (u != null)
                {
                    throw new BussinessException(1);
                }
                //Valida si es mayor de edad
                if (usuario.Edad < 18)
                {
                    throw new BussinessException(2);

                }
                usuario.Estado = "Pendiente";
                usuario.OTP = 1;
                usuario.Latitud = "123.313123";
                usuario.Longitud = "123.313123";
                usuario.PersoneriaJuridica = "NULL";
                usuario.PermisoOperaciones = "NULL";

                var contrasenna = new Contrasennas
                {
                    Contrasenna = usuario.ContrassenaActual,
                    Correo = usuario.Correo,
                    Fecha = DateTime.Now
                };


                //Validacion del Rol y de los campos necesarios


                crudUsuarios.Create(usuario);
                crudContrasennas.Create(contrasenna);
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

        public List<Usuarios> RetrieveAllSolicitudes() {
            try {
                return crudUsuarios.RetrieveAllSolicitudes<Usuarios>();
            } catch(Exception ex) {
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
           /* try
            {*/
                crudUsuarios.Update(usuarios);
            /*}
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }*/
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
