using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class ContrasennaMapper : EntityMapper, ISqlStatements, IObjectMapper
    {
        private const string DB_COL_FECHA = "FECHA";
        private const string DB_COL_CONTRASENNA = "CONTRASENNA";
        private const string DB_COL_CORREO = "CORREO";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var contrasenna = new Contrasennas
            {
                Fecha = GetDateValue(row, DB_COL_FECHA),
                Contrasenna = GetStringValue(row, DB_COL_CONTRASENNA),
                Correo = GetStringValue(row, DB_COL_CORREO)
            };

            return contrasenna;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var contrasenna = BuildObject(row);
                lstResults.Add(contrasenna);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_CONTRASENNA_PR" };

            var c = (Contrasennas)entity;
            operation.AddVarcharParam(DB_COL_CORREO, c.Correo);
            operation.AddDateTimeParam(DB_COL_FECHA, c.Fecha);
            operation.AddVarcharParam(DB_COL_CONTRASENNA, c.Contrasenna);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_CONTRASENNA_PR" };

            var c = (Contrasennas)entity;
            operation.AddVarcharParam(DB_COL_CORREO, c.Correo);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_CONTRASENNA_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CONTRASENNA_PR" };

            var c = (Contrasennas)entity;
            operation.AddVarcharParam(DB_COL_CORREO, c.Correo);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_CONTRASENNA_PR" };

            var c = (Contrasennas)entity;
            operation.AddDateTimeParam(DB_COL_FECHA, c.Fecha);
            operation.AddVarcharParam(DB_COL_CONTRASENNA, c.Contrasenna);
            operation.AddVarcharParam(DB_COL_CORREO, c.Correo);

            return operation;
        }
    }
}
