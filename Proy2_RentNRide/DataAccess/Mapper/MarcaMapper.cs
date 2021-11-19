using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entities_POJO;

namespace DataAccess.Mapper {
    public class MarcaMapper : EntityMapper, ISqlStatements, IObjectMapper {
        private const string DB_COL_ID = "id";
        private const string DB_COL_NOMBRE = "nombre";
        private const string DB_COL_ESTADO = "estado";

        public BaseEntity BuildObject(Dictionary<string, object> row) {
            var marca = new Marca {
                Id = GetIntValue(row, DB_COL_ID),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Estado = GetStringValue(row, DB_COL_ESTADO)
            };

            return marca;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows) {
            var lstResults = new List<BaseEntity>();

            foreach(var row in lstRows) {
                var marca = BuildObject(row);
                lstResults.Add(marca);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "CRE_MARCA_PR" };

            var c = (Marca) entity;

            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "DEL_MARCA_PR" };

            var c = (Marca) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement() {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_MARCAS_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "RET_MARCA_PR" };

            var c = (Marca) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "UPD_MARCA_PR" };

            var c = (Marca) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);

            return operation;
        }
    }
}
