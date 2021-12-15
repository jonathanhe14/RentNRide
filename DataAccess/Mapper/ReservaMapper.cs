using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class ReservaMapper : EntityMapper, ISqlStatements, IObjectMapper
    {
        private const string DB_COL_ID_VEHICULO = "ID_VEHICULO";
        private const string DB_COL_ID_RESERVA = "ID_RESERVA";
        private const string DB_COL_FECHA = "FECHA";
        private const string DB_COL_FECHA_RESERVA = "FECHA_RESERVA";
        private const string DB_COL_HORA_INICIO = "HORA_INICIO";
        private const string DB_COL_HORA_FIN = "HORA_FIN";
        private const string DB_COL_USUARIO = "USUARIO";
        private const string DB_COL_SOCIO = "SOCIO";
        private const string DB_COL_TARIFA = "TARIFA";
        private const string DB_COL_C_COMISION = "C_COMISION";
        private const string DB_COL_C_MAL_ESTADO = "C_MAL_ESTADO";
        private const string DB_COL_C_ENTREGA = "C_ENTREGA";
        private const string DB_COL_C_KM_EXCEDIDO = "C_KM_EXCEDIDO";
        private const string DB_COL_KM_INICIAL = "KM_INICIAL";
        private const string DB_COL_KM_FINAL = "KM_FINAL";
        private const string DB_COL_LATITUD = "LATITUD";
        private const string DB_COL_LONGITUD = "LONGITUD";
        private const string DB_COL_SOLICITUD = "SOLICITUD";
        private const string DB_COL_CALIF_SOCIO = "CALIF_SOCIO";
        private const string DB_COL_CALIF_USUARIO = "CALIF_USUARIO";
        private const string DB_COL_CODIGO_QR = "CODIGO_QR";
        ////////////////////////////////
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_HORA_FINAL = "HORA_FINAL";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_RESERVA_PR" };

            var c = (Reserva)entity;
            operation.AddIntParam(DB_COL_ID_VEHICULO, c.Id_Vehiculo);
            operation.AddDateTimeParam(DB_COL_FECHA, c.Fecha);
            operation.AddDateTimeParam(DB_COL_FECHA_RESERVA, c.FechaReserva);
            operation.AddVarcharParam(DB_COL_HORA_INICIO, c.HoraInicio);
            operation.AddVarcharParam(DB_COL_HORA_FIN, c.HoraFin);
            operation.AddVarcharParam(DB_COL_USUARIO, c.Usuario);
            operation.AddVarcharParam(DB_COL_SOCIO, c.Socio);
            operation.AddDecimalParam(DB_COL_TARIFA, c.Tarifa);
            operation.AddDecimalParam(DB_COL_C_COMISION, c.Comision);
            operation.AddDecimalParam(DB_COL_C_MAL_ESTADO, c.MalEstado);
            operation.AddDecimalParam(DB_COL_C_ENTREGA, c.Entrega);
            operation.AddDecimalParam(DB_COL_C_KM_EXCEDIDO, c.KmExcedido);
            operation.AddDecimalParam(DB_COL_KM_INICIAL, c.KmInicial);
            operation.AddDecimalParam(DB_COL_KM_FINAL, c.KmFinal);
            operation.AddVarcharParam(DB_COL_LATITUD, c.Latitud);
            operation.AddVarcharParam(DB_COL_LONGITUD, c.Longitud);
            operation.AddVarcharParam(DB_COL_SOLICITUD, c.Solicitud);
            operation.AddIntParam(DB_COL_CALIF_SOCIO, c.CalifSocio);
            operation.AddIntParam(DB_COL_CALIF_USUARIO, c.CalifUsuario);
            operation.AddVarcharParam(DB_COL_CODIGO_QR, c.CodigoQR);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_RESERVA_PR" };

            var c = (Reserva)entity;
            operation.AddIntParam(DB_COL_ID_RESERVA, c.Id_Reserva);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_RESERVA_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_RESERVA_PR" };

            var c = (Reserva)entity;
            operation.AddVarcharParam(DB_COL_USUARIO, c.Usuario);
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_RESERVA_PR" };

            var c = (Reserva)entity;
            operation.AddIntParam(DB_COL_ID_VEHICULO, c.Id_Vehiculo);
            operation.AddIntParam(DB_COL_ID_RESERVA, c.Id_Reserva);
            operation.AddDateTimeParam(DB_COL_FECHA, c.Fecha);
            operation.AddDateTimeParam(DB_COL_FECHA_RESERVA, c.FechaReserva);
            operation.AddVarcharParam(DB_COL_HORA_INICIO, c.HoraInicio);
            operation.AddVarcharParam(DB_COL_HORA_FIN, c.HoraFin);
            operation.AddVarcharParam(DB_COL_USUARIO, c.Usuario);
            operation.AddVarcharParam(DB_COL_SOCIO, c.Socio);
            operation.AddDecimalParam(DB_COL_TARIFA, c.Tarifa);
            operation.AddDecimalParam(DB_COL_C_COMISION, c.Comision);
            operation.AddDecimalParam(DB_COL_C_MAL_ESTADO, c.MalEstado);
            operation.AddDecimalParam(DB_COL_C_ENTREGA, c.Entrega);
            operation.AddDecimalParam(DB_COL_C_KM_EXCEDIDO, c.KmExcedido);
            operation.AddDecimalParam(DB_COL_KM_INICIAL, c.KmInicial);
            operation.AddDecimalParam(DB_COL_KM_FINAL, c.KmFinal);
            operation.AddVarcharParam(DB_COL_LATITUD, c.Latitud);
            operation.AddVarcharParam(DB_COL_LONGITUD, c.Longitud);
            operation.AddVarcharParam(DB_COL_SOLICITUD, c.Solicitud);
            operation.AddIntParam(DB_COL_CALIF_SOCIO, c.CalifSocio);
            operation.AddIntParam(DB_COL_CALIF_USUARIO, c.CalifUsuario);
            operation.AddVarcharParam(DB_COL_CODIGO_QR, c.CodigoQR);

            return operation;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var reserva = new Reserva
            {
                Id_Vehiculo = GetIntValue(row, DB_COL_ID_VEHICULO),
                Id_Reserva = GetIntValue(row, DB_COL_ID_RESERVA),
                Fecha = GetDateValue(row, DB_COL_FECHA),
                FechaReserva = GetDateValue(row, DB_COL_FECHA_RESERVA),
                HoraInicio = GetStringValue(row, DB_COL_HORA_INICIO),
                HoraFin = GetStringValue(row, DB_COL_HORA_FIN),
                Usuario = GetStringValue(row, DB_COL_USUARIO),
                Socio = GetStringValue(row, DB_COL_SOCIO),
                Tarifa = GetDecimalValue(row, DB_COL_TARIFA),
                Comision = GetDecimalValue(row, DB_COL_C_COMISION),
                MalEstado = GetDecimalValue(row, DB_COL_C_MAL_ESTADO),
                Entrega = GetDecimalValue(row, DB_COL_C_ENTREGA),
                KmExcedido = GetDecimalValue(row, DB_COL_C_KM_EXCEDIDO),
                KmInicial = GetDecimalValue(row, DB_COL_KM_INICIAL),
                KmFinal = GetDecimalValue(row, DB_COL_KM_FINAL),
                Latitud = GetStringValue(row, DB_COL_LATITUD),
                Longitud = GetStringValue(row, DB_COL_LONGITUD),
                Solicitud = GetStringValue(row, DB_COL_SOLICITUD),
                CalifSocio = GetIntValue(row, DB_COL_CALIF_SOCIO),
                CalifUsuario = GetIntValue(row, DB_COL_CALIF_USUARIO),
                CodigoQR = GetStringValue(row, DB_COL_CODIGO_QR)
            };

            return reserva;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var reserva = BuildObject(row);
                lstResults.Add(reserva);
            }

            return lstResults;
        }

        public SqlOperation GetDisponibilidad(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_DISPONIBILIDAD_PR" };
            var c = (ConsultaReserva)entity;
            operation.AddIntParam(DB_COL_ID_VEHICULO, c.Id_Vehiculo);
            operation.AddDateTimeParam(DB_COL_FECHA_RESERVA, c.Fecha_Reserva);
            return operation;
        }

        public BaseEntity BuildObjectConsulta(Dictionary<string, object> row)
        {
            var consulta = new ConsultaReserva
            {
                Id_Vehiculo = GetIntValue(row, DB_COL_ID_VEHICULO),
                Fecha_Reserva = GetDateValue(row, DB_COL_FECHA_RESERVA),
                Hora_Inicio = GetStringValue(row, DB_COL_HORA_INICIO),
                Hora_Fin = GetStringValue(row, DB_COL_HORA_FINAL),
                Estado = GetStringValue(row, DB_COL_ESTADO)
            };

            return consulta;
        }

        public List<BaseEntity> BuildObjectsConsulta(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var reserva = BuildObjectConsulta(row);
                lstResults.Add(reserva);
            }

            return lstResults;
        }
    }
}
