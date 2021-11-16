using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    class HorarioMapper : EntityMapper, ISqlStatements, IObjectMapper
    {


        private const string DB_COL_ID = "P_id";
        private const string DB_COL_DIA = "P_dia";
        private const string DB_COL_HORA_INICIO = "P_inicio";
        private const string DB_COL_HORA_FIN = "P_fin";
        

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_HORARIO_PR" };

            var c = (Horario)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddDateTimeParam(DB_COL_DIA, c.Dia);
            operation.AddVarcharParam(DB_COL_HORA_INICIO, c.horaInicio);
            operation.AddVarcharParam(DB_COL_HORA_FIN, c.horaFinal);
            

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_HORARIO_PR" };

            var c = (Horario)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_HORARIO_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_HORARIO_PR" };

            var c = (Horario)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddDateTimeParam(DB_COL_DIA, c.Dia);
            operation.AddVarcharParam(DB_COL_HORA_INICIO, c.horaInicio);
            operation.AddVarcharParam(DB_COL_HORA_FIN, c.horaFinal);


            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_HORARIO_PR" };

            var c = (Horario)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
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
            var horario = new Horario
            {
                Id = GetIntValue(row, DB_COL_ID),
                Dia = GetDateValue(row, DB_COL_DIA),
                horaInicio = GetStringValue(row, DB_COL_HORA_INICIO),
                horaFinal = GetStringValue(row, DB_COL_HORA_FIN),
            };

            return horario;
        }

    }
}
