using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entities_POJO;

namespace DataAccess.Mapper {
    public class TipoVehiculoMapper : EntityMapper, ISqlStatements, IObjectMapper {
        private const string DB_COL_ID = "id";
        private const string DB_COL_NOMBRE = "nombre";
        private const string DB_COL_ESTADO = "estado";

        public BaseEntity BuildObject(Dictionary<string, object> row) {
            var tipoVehiculo = new TipoVehiculo {
                Id = GetIntValue(row, DB_COL_ID),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Estado = GetStringValue(row, DB_COL_ESTADO)
            };

            return tipoVehiculo;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows) {
            var lstResults = new List<BaseEntity>();

            foreach(var row in lstRows) {
                var tipoVehiculo = BuildObject(row);
                lstResults.Add(tipoVehiculo);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "CRE_TIPO_VEHICULO_PR" };

            var c = (TipoVehiculo) entity;

            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "DEL_TIPO_VEHICULO_PR" };

            var c = (TipoVehiculo) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement() {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_TIPO_VEHICULOS_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "RET_TIPO_VEHICULO_PR" };

            var c = (TipoVehiculo) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "UPD_TIPO_VEHICULO_PR" };

            var c = (TipoVehiculo) entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);

            return operation;
        }
    }
}
