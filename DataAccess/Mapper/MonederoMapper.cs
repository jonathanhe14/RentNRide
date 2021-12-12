using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class MonederoMapper : EntityMapper, ISqlStatements, IObjectMapper
    {
        private const string DB_COL_ID_MONEDERO = "ID_MONEDERO";
        private const string DB_COL_ID_USUARIO = "ID_USUARIO";
        private const string DB_COL_SALDO = "SALDO";
        private const string DB_COL_FECHA_CORTE = "FECHA_CORTE";
        private const string DB_COL_FECHA_EXPIRACION = "FECHA_EXPIRACION";
        private const string DB_COL_INFO_MONEDERO = "INFO_MONEDERO";
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var monedero = new Monedero
            {
                IdMonedero = GetIntValue(row, DB_COL_ID_MONEDERO),
                IdUsuario = GetStringValue(row, DB_COL_ID_USUARIO),
                Saldo = GetDecimalValue(row, DB_COL_SALDO),
                FechaCorte = GetDateValue(row, DB_COL_FECHA_CORTE),
                FechaExpiracion = GetDateValue(row, DB_COL_FECHA_EXPIRACION),
                InfoMonedero = GetStringValue(row, DB_COL_INFO_MONEDERO),
            };

            return monedero;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var monedero = BuildObject(row);
                lstResults.Add(monedero);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_MONEDERO_PR" };

            var m = (Monedero)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, m.IdUsuario);
            operation.AddDecimalParam(DB_COL_SALDO, m.Saldo);
            operation.AddDateTimeParam(DB_COL_FECHA_CORTE, m.FechaCorte);
            operation.AddDateTimeParam(DB_COL_FECHA_EXPIRACION, m.FechaExpiracion);
            operation.AddVarcharParam(DB_COL_INFO_MONEDERO, m.InfoMonedero);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "" };
            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_MONEDERO_PR" };

            var c = (Monedero)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, c.IdUsuario);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_MONEDERO_PR" };

            var m = (Monedero)entity;

            operation.AddVarcharParam(DB_COL_ID_USUARIO, m.IdUsuario);
            operation.AddDecimalParam(DB_COL_SALDO, m.Saldo);
            operation.AddDateTimeParam(DB_COL_FECHA_CORTE, m.FechaCorte);
            operation.AddDateTimeParam(DB_COL_FECHA_EXPIRACION, m.FechaExpiracion);
            operation.AddVarcharParam(DB_COL_INFO_MONEDERO, m.InfoMonedero);

            return operation;
        }
    }
}
