using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class MembresiasUsuarioMapper : EntityMapper, ISqlStatements, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_ID_USUARIO = "ID_USUARIO";
        private const string DB_COL_ID_MEMBRESIA = "ID_MEMBRESIA";
        private const string DB_COL_ACTIVO = "ACTIVO";
        private const string DB_COL_COMPROBANTE = "COMPROBANTE";


        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var membresiaU = new MembresiasUsuario
            {
                Id = GetIntValue(row, DB_COL_ID),
                IdUsuario = GetIntValue(row, DB_COL_ID_USUARIO),
                IdMembresia = GetIntValue(row, DB_COL_ID_MEMBRESIA),
                Estado = GetStringValue(row, DB_COL_ACTIVO),
                Comprobante = GetStringValue(row, DB_COL_COMPROBANTE)

            };

            return membresiaU;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var membresia = BuildObject(row);
                lstResults.Add(membresia);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }





        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_MEMBRESIA_USUARIO_PR" };

            var c = (MembresiasUsuario)entity;
            operation.AddIntParam(DB_COL_ID_USUARIO, c.IdUsuario);
            operation.AddIntParam(DB_COL_ID_MEMBRESIA, c.IdMembresia);
            operation.AddVarcharParam(DB_COL_ACTIVO, c.Estado);
            operation.AddVarcharParam(DB_COL_COMPROBANTE, c.Comprobante);


            return operation;
        }


    }
}
