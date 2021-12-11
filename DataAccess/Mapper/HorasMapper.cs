using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class HorasMapper : EntityMapper, ISqlStatements, IObjectMapper
    {

        private const string DB_COL_ID_VEHICULO = "ID_VEHICULO";
        private const string DB_COL_ID_HORARIO = "ID_HORARIO";
        private const string DB_COL_ID_HORA = "ID_HORA";
        private const string DB_COL_DIA = "DIA";
        private const string DB_COL_HORA_INICIO = "HORA_INICIO";
        private const string DB_COL_HORA_FINAL = "HORA_FINAL";
        private const string DB_COL_DISPONIBILIDAD = "DISPONIBILIDAD";
        private const string DB_COL_ESTADO = "ESTADO";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var horas = new Horas
            {
                Id_Vehiculo = GetIntValue(row, DB_COL_ID_VEHICULO),
                Id_Horario = GetIntValue(row, DB_COL_ID_HORARIO),
                Id_Hora = GetIntValue(row, DB_COL_ID_HORA),
                Dia = GetIntValue(row, DB_COL_DIA),
                Hora_Inicio = GetStringValue(row, DB_COL_HORA_INICIO),
                Hora_Final = GetStringValue(row, DB_COL_HORA_FINAL),
                Disponibilidad = GetStringValue(row, DB_COL_DISPONIBILIDAD),
                Estado = GetStringValue(row, DB_COL_ESTADO)
            };

            return horas;
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

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_HORAS_PR" };

            var c = (Horas)entity;
            operation.AddIntParam(DB_COL_ID_VEHICULO, c.Id_Vehiculo);
            operation.AddIntParam(DB_COL_ID_HORARIO, c.Id_Horario);
            operation.AddIntParam(DB_COL_DIA, c.Dia);
            operation.AddVarcharParam(DB_COL_HORA_INICIO, c.Hora_Inicio);
            operation.AddVarcharParam(DB_COL_HORA_FINAL, c.Hora_Final);
            operation.AddVarcharParam(DB_COL_DISPONIBILIDAD, c.Disponibilidad);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_HORAS_PR" };

            var c = (Horas)entity;
            operation.AddIntParam(DB_COL_ID_HORARIO, c.Id_Horario);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_HORAS_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_HORAS_IDVEHICULO_PR" };

            var c = (Horas)entity;
            operation.AddIntParam(DB_COL_ID_VEHICULO, c.Id_Vehiculo);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_HORAS_HORARIO_PR" };

            var c = (Horas)entity;
            operation.AddIntParam(DB_COL_ID_VEHICULO, c.Id_Vehiculo);
            operation.AddIntParam(DB_COL_ID_HORARIO, c.Id_Horario);
            operation.AddIntParam(DB_COL_ID_HORA, c.Id_Hora);
            operation.AddIntParam(DB_COL_DIA, c.Dia);
            operation.AddVarcharParam(DB_COL_HORA_INICIO, c.Hora_Inicio);
            operation.AddVarcharParam(DB_COL_HORA_FINAL, c.Hora_Final);
            operation.AddVarcharParam(DB_COL_DISPONIBILIDAD, c.Disponibilidad);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);

            return operation;
        }
    }
}
