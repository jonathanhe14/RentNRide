using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Crud;
using Exceptions;

namespace CoreAPI
{
    public class RolesUManager : BaseEntity
    {
        private UsuariosRolCrudFactory crudRolesUsuarios;

        public RolesUManager()
        {
            crudRolesUsuarios = new UsuariosRolCrudFactory();

        }

        public void Create(UsuariosRol rol)
        {
            try
            {

                crudRolesUsuarios.Create(rol);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<UsuariosRol> RetrieveAll()
        {
            try
            {
                return crudRolesUsuarios.RetrieveAll<UsuariosRol>();
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }

        }

        public UsuariosRol RetrieveById(Usuarios usuarios)
        {
            try
            {
                return crudRolesUsuarios.Retrieve<UsuariosRol>(usuarios);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }

        public List<UsuariosRol> RetrieveAllById(Usuarios usuarios)
        {
            try
            {
                return crudRolesUsuarios.RetrieveAllByUser<UsuariosRol>(usuarios);
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
                crudRolesUsuarios.Update(usuarios);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }

    }
}
