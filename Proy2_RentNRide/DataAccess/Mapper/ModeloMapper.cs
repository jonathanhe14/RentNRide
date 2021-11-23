using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entities_POJO;

namespace DataAccess.Mapper {
    public class ModeloMapper : EntityMapper, ISqlStatements, IObjectMapper {
        private const string DB_COL_ID = "id";
        private const string DB_COL_NOMBRE = "nombre";
        private const string DB_COL_ESTADO = "estado";
        private const string DB_COL_MARCA = "id_marca";

        public BaseEntity BuildObject(Dictionary<string, object> row) {
            var modelo = new Modelo {
                Id = GetIntValue(row, DB_COL_ID),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                Marca = GetIntValue(row, DB_COL_MARCA)
            };

            return modelo;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows) {
            var lstResults = new List<BaseEntity>();

            foreach(var row in lstRows) {
                var modelo = BuildObject(row);
                lstResults.Add(modelo);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "CRE_MODELO_PR" };

            var c = (Modelo) entity;

            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);
            operation.AddIntParam(DB_COL_MARCA, c.Marca);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "DEL_MODELO_PR" };

            var c = (Modelo) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement() {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_MODELOS_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "RET_MODELO_PR" };

            var c = (Modelo) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "UPD_MODELO_PR" };

            var c = (Modelo) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
          
            operation.AddIntParam(DB_COL_MARCA, c.Marca);

            return operation;
        }
    }
}
