using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entities_POJO;

namespace DataAccess.Mapper {
    public class TipoCombustibleMapper : EntityMapper, ISqlStatements, IObjectMapper {
        private const string DB_COL_ID = "id";
        private const string DB_COL_NOMBRE = "nombre";
        private const string DB_COL_ESTADO = "estado";

        public BaseEntity BuildObject(Dictionary<string, object> row) {
            var tipoCombustible = new TipoCombustible {
                Id = GetIntValue(row, DB_COL_ID),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Estado = GetStringValue(row, DB_COL_ESTADO)
            };

            return tipoCombustible;
        }   

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows) {
            var lstResults = new List<BaseEntity>();

            foreach(var row in lstRows) {
                var tipoCombustible = BuildObject(row);
                lstResults.Add(tipoCombustible);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "CRE_TIPO_COMBUSTIBLE_PR" };

            var c = (TipoCombustible) entity;

            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "DEL_TIPO_COMBUSTIBLE_PR" };

            var c = (TipoCombustible) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement() {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_TIPO_COMBUSTIBLE_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "RET_TIPO_COMBUSTIBLE_PR" };

            var c = (TipoCombustible) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "UPD_TIPO_COMBUSTIBLE_PR" };

            var c = (TipoCombustible) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);

            return operation;
        }
    }
}
