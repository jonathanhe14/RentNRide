using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entities_POJO;

namespace DataAccess.Mapper {
    public class MembresiaMapper : EntityMapper, ISqlStatements, IObjectMapper {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_MONTO_MENSUAL = "MONTO_MENSUAL";
        private const string DB_COL_COMISION_TRANSACCION = "COMISION_TRANSACCION";
        private const string DB_COL_FECHA_CREACION = "FECHA_CREACION";
        private const string DB_COL_NUM_DIAS = "NUM_DIAS";
        private const string DB_COL_ACTIVO = "ACTIVO";

        public BaseEntity BuildObject(Dictionary<string, object> row) {
            var membresia = new Membresias {
                Id = GetIntValue(row, DB_COL_ID),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                MontoMensual = GetDecimalValue(row, DB_COL_MONTO_MENSUAL),
                ComisionTransaccion = GetDecimalValue(row, DB_COL_COMISION_TRANSACCION),
                FechaCreacion = GetDateValue(row, DB_COL_FECHA_CREACION),
                NumDias = GetIntValue(row, DB_COL_NUM_DIAS),
                Activo = GetStringValue(row, DB_COL_ACTIVO)
            };

            return membresia;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows) {
            var lstResults = new List<BaseEntity>();

            foreach(var row in lstRows) {
                var membresia = BuildObject(row);
                lstResults.Add(membresia);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "CRE_MEMBRESIA_PR" };

            var c = (Membresias) entity;
      
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddDecimalParam(DB_COL_MONTO_MENSUAL, c.MontoMensual);
            operation.AddDecimalParam(DB_COL_COMISION_TRANSACCION, c.ComisionTransaccion);
            operation.AddDateTimeParam(DB_COL_FECHA_CREACION, c.FechaCreacion);
            operation.AddIntParam(DB_COL_NUM_DIAS, c.NumDias);
            operation.AddVarcharParam(DB_COL_ACTIVO, c.Activo);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "DEL_MEMBRESIA_PR" };

            var c = (Membresias) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement() {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_MEMBRESIAS_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "RET_MEMBRESIA_PR" };

            var c = (Membresias) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "UPD_MEMBRESIA_PR" };

            var c = (Membresias) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddDecimalParam(DB_COL_MONTO_MENSUAL, c.MontoMensual);
            operation.AddDecimalParam(DB_COL_COMISION_TRANSACCION, c.ComisionTransaccion);
            operation.AddDateTimeParam(DB_COL_FECHA_CREACION, c.FechaCreacion);
            operation.AddIntParam(DB_COL_NUM_DIAS, c.NumDias);
  

            return operation;
        }

        public BaseEntity BuildObjectId(Dictionary<string, object> row)
        {
            var membresia = new Membresias
            {
                Id = GetIntValue(row, DB_COL_ID)
            };

            return membresia;
        }

    }
}
