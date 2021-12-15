using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class FacturaMapper : EntityMapper, ISqlStatements, IObjectMapper
    {

        private const string DB_COL_ID_FACTURA = "ID_FACTURA";
        private const string DB_COL_FECHA_EMISION = "FECHA_EMISION";
        private const string DB_COL_IDENTIFICACION = "IDENTIFICACION";
        private const string DB_COL_CONSECUTIVO = "CONSECUTIVO";
        private const string DB_COL_CLAVE = "CLAVE";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_CORREO = "CORREO";
        private const string DB_COL_MONTO = "MONTO";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_FACTURA_PR" };

            var c = (Factura)entity;
            operation.AddDateTimeParam(DB_COL_FECHA_EMISION, c.FechaEmision);
            operation.AddVarcharParam(DB_COL_IDENTIFICACION, c.Identificacion);
            operation.AddVarcharParam(DB_COL_CONSECUTIVO, c.Consecutivo);
            operation.AddVarcharParam(DB_COL_CLAVE, c.Clave);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddVarcharParam(DB_COL_CORREO, c.Correo);
            operation.AddDecimalParam(DB_COL_MONTO, c.Monto);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_FACTURA_PR" };

            var c = (Factura)entity;
            operation.AddIntParam(DB_COL_ID_FACTURA, c.IdFactura);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_FACTURA_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_FACTURA_PR" };

            var c = (Factura)entity;
            operation.AddIntParam(DB_COL_ID_FACTURA, c.IdFactura);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_FACTURA_PR" };

            var c = (Factura)entity;
            operation.AddIntParam(DB_COL_ID_FACTURA, c.IdFactura);
            operation.AddDateTimeParam(DB_COL_FECHA_EMISION, c.FechaEmision);
            operation.AddVarcharParam(DB_COL_IDENTIFICACION, c.Identificacion);
            operation.AddVarcharParam(DB_COL_CONSECUTIVO, c.Consecutivo);
            operation.AddVarcharParam(DB_COL_CLAVE, c.Clave);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddVarcharParam(DB_COL_CORREO, c.Correo);
            operation.AddDecimalParam(DB_COL_MONTO, c.Monto);

            return operation;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var factura = new Factura
            {
                IdFactura = GetIntValue(row, DB_COL_ID_FACTURA),
                FechaEmision = GetDateValue(row, DB_COL_FECHA_EMISION),
                Identificacion = GetStringValue(row, DB_COL_IDENTIFICACION),
                Consecutivo = GetStringValue(row, DB_COL_CONSECUTIVO),
                Clave = GetStringValue(row, DB_COL_CLAVE),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Correo = GetStringValue(row, DB_COL_CORREO),
                Monto = GetDecimalValue(row, DB_COL_MONTO)
            };

            return factura;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var vehiculo = BuildObject(row);
                lstResults.Add(vehiculo);
            }

            return lstResults;
        }
    }
}
