using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    class MarcaMapper : EntityMapper, ISqlStatements, IObjectMapper
    {


        private const string DB_COL_ID = "id";
        private const string DB_COL_NOMBRE = "nombre";
        private const string DB_COL_ESTADO = "estado";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_MARCA_PR" };

            var c = (VehiOpcion)entity;
            operation.AddIntParam(DB_COL_ID, c.id);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.nombre);
            operation.AddVarcharParam(DB_COL_ESTADO, c.estado);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_MARCA_PR" };

            var c = (VehiOpcion)entity;
            operation.AddIntParam(DB_COL_ID, c.id);


            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_MARCAS_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_MARCA_PR" };

            var c = (VehiOpcion)entity;
            operation.AddIntParam(DB_COL_ID, c.id);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.nombre);
            operation.AddVarcharParam(DB_COL_ESTADO, c.estado);


            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_MARCA_PR" };

            var c = (VehiOpcion)entity;
            operation.AddIntParam(DB_COL_ID, c.id);
            return operation;
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

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var vehiculo = new VehiOpcion
            {
                id = GetIntValue(row, DB_COL_ID),
                nombre = GetStringValue(row, DB_COL_NOMBRE),
                estado = GetStringValue(row, DB_COL_ESTADO),
            };

            return vehiculo;
        }

    }
}
