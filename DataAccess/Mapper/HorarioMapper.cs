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


        private const string DB_COL_Id_Vehiculo = "Id_Vehiculo";
        private const string DB_COL_HORA_INICIO = "HORA_INICIO";
        private const string DB_COL_HORA_FIN = "HORA_FIN";
        private const string DB_COL_ID = "id";
        private const string DB_COL_DISPONIBILIDAD = "DISPONIBILIDAD";
        private const string DB_COL_DIA_INICIAL = "DIA_INICIAL";
        private const string DB_COL_DIA_FINAL = "DIA_FINAL";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_HORARIO_PR" };

            var c = (Horario)entity;
            operation.AddIntParam(DB_COL_Id_Vehiculo, c.Id_Vehiculo);
            operation.AddVarcharParam(DB_COL_HORA_INICIO, c.horaInicio);
            operation.AddVarcharParam(DB_COL_HORA_FIN, c.horaFinal);
            operation.AddVarcharParam(DB_COL_DISPONIBILIDAD, c.Disponibilidad);
            operation.AddIntParam(DB_COL_DIA_INICIAL, c.DiaInicial);
            operation.AddIntParam(DB_COL_DIA_FINAL, c.DiaFinal);


            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_HORARIO_PR" };

            var c = (Horario)entity;
            operation.AddIntParam(DB_COL_Id_Vehiculo, c.Id_Vehiculo);

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
            operation.AddIntParam(DB_COL_Id_Vehiculo, c.Id_Vehiculo);
            operation.AddIntParam(DB_COL_DIA_INICIAL, c.DiaInicial);
            operation.AddIntParam(DB_COL_DIA_FINAL, c.DiaFinal);
            operation.AddVarcharParam(DB_COL_HORA_INICIO, c.horaInicio);
            operation.AddVarcharParam(DB_COL_HORA_FIN, c.horaFinal);
            operation.AddVarcharParam(DB_COL_DISPONIBILIDAD, c.Disponibilidad);
            operation.AddIntParam(DB_COL_ID, c.Id);


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
                Id_Vehiculo = GetIntValue(row, DB_COL_Id_Vehiculo),
                horaInicio = GetStringValue(row, DB_COL_HORA_INICIO),
                horaFinal = GetStringValue(row, DB_COL_HORA_FIN),
                Id = GetIntValue(row, DB_COL_ID),
                Disponibilidad = GetStringValue(row, DB_COL_DISPONIBILIDAD),
                DiaInicial = GetIntValue(row, DB_COL_DIA_INICIAL),
                DiaFinal = GetIntValue(row, DB_COL_DIA_FINAL)
            };

            return horario;
        }

    }
}
