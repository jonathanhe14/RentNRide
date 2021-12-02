using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class UsuariosRolMapper : EntityMapper, ISqlStatements, IObjectMapper

    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_ID_ROL = "ID_ROL";
        private const string DB_COL_ID_USUARIO = "ID_USUARIO";
        private const string DB_COL_ACTIVO = "ACTIVO";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_ROLES_USUARIO_PR" };

            var ur = (UsuariosRol)entity;
            operation.AddIntParam(DB_COL_ID_ROL, ur.IdRol);
            operation.AddVarcharParam(DB_COL_ID_USUARIO, ur.IdUsuario);
            operation.AddVarcharParam(DB_COL_ACTIVO, ur.Estado);


            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_ROLES_USUARIO_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ROL_USUARIO_PR" };

            var ur = (Usuarios)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, ur.Correo);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            try
            {
                var usuarioRol = new UsuariosRol
                {
                    Id = GetIntValue(row, DB_COL_ID),
                    IdRol = GetIntValue(row, DB_COL_ID_ROL),
                    IdUsuario = GetStringValue(row, DB_COL_ID_USUARIO),
                    Estado = GetStringValue(row, DB_COL_ACTIVO),


                };
                return usuarioRol;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }




        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {

                var usuarios = BuildObject(row);
                lstResults.Add(usuarios);
            }

            return lstResults;
        }




    }
}