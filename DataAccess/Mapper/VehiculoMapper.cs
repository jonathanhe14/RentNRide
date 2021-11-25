using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    class VehiculoMapper : EntityMapper, ISqlStatements, IObjectMapper
    {

        private const string DB_COL_ID = "id_vehiculo";
        private const string DB_COL_TIPO = "tipo_vehiculo";
        private const string DB_COL_TIPO_COMBUSTIBLE = "tipo_combustible";
        private const string DB_COL_MODELO = "modelo";
        private const string DB_COL_MARCA = "marca";
        private const string DB_COL_KILOMETRAJE = "kilometraje";
        private const string DB_COL_EXCEDIDO = "cargo_km_exedido";
        private const string DB_COL_MAL_ESTADO = "cargo_mal_estado";
        private const string DB_COL_LATITUD = "latitud";
        private const string DB_COL_LONGITUD = "longitud";
        private const string DB_COL_LUGAR_DIFF = "cargo_lugar_diferente";
        private const string DB_COL_TARIFA = "tarifa";
        private const string DB_COL_ACEPT_INMD = "aceptacion_inmediata";
        private const string DB_COL_ESTADO = "estado";
        private const string DB_COL_IMAGEN = "imagen";
        private const string DB_COL_USUARIO = "id_usuario";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_VEHICULO_PR" };

            var c = (Vehiculo)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam(DB_COL_TIPO, c.Tipo);
            operation.AddIntParam(DB_COL_TIPO_COMBUSTIBLE, c.Combustible);
            operation.AddIntParam(DB_COL_MODELO, c.Modelo);
            operation.AddIntParam(DB_COL_MARCA, c.Marca);
            operation.AddDoubleParam(DB_COL_KILOMETRAJE, c.Kilometraje);
            operation.AddDoubleParam(DB_COL_EXCEDIDO, c.cKmExcedido);
            operation.AddDoubleParam(DB_COL_MAL_ESTADO, c.cMalEstado);
            operation.AddVarcharParam(DB_COL_LATITUD, c.Latitud);
            operation.AddVarcharParam(DB_COL_LONGITUD, c.Longitud);
            operation.AddDoubleParam(DB_COL_LUGAR_DIFF, c.cLugarDiferente);
            operation.AddDoubleParam(DB_COL_TARIFA, c.Tarifa);
            operation.AddVarcharParam(DB_COL_ACEPT_INMD, c.AccptInmediata);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);
            operation.AddVarcharParam(DB_COL_IMAGEN, c.Imagen);
            operation.AddVarcharParam(DB_COL_USUARIO, c.idUsuario);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_VEHICULO_PR" };

            var c = (Vehiculo)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_VEHICULO_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_VEHICULO_PR" };

            var c = (Vehiculo)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam(DB_COL_TIPO, c.Tipo);
            operation.AddIntParam(DB_COL_TIPO_COMBUSTIBLE, c.Combustible);
            operation.AddIntParam(DB_COL_MODELO, c.Modelo);
            operation.AddIntParam(DB_COL_MARCA, c.Marca);
            operation.AddDoubleParam(DB_COL_KILOMETRAJE, c.Kilometraje);
            operation.AddDoubleParam(DB_COL_EXCEDIDO, c.cKmExcedido);
            operation.AddDoubleParam(DB_COL_MAL_ESTADO, c.cMalEstado);
            operation.AddVarcharParam(DB_COL_LATITUD, c.Latitud);
            operation.AddVarcharParam(DB_COL_LONGITUD, c.Longitud);
            operation.AddDoubleParam(DB_COL_LUGAR_DIFF, c.cLugarDiferente);
            operation.AddDoubleParam(DB_COL_TARIFA, c.Tarifa);
            operation.AddVarcharParam(DB_COL_ACEPT_INMD, c.AccptInmediata);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);
            operation.AddVarcharParam(DB_COL_IMAGEN, c.Imagen);
            operation.AddVarcharParam(DB_COL_USUARIO, c.idUsuario);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_VEHICULO_PR" };

            var c = (Vehiculo)entity;
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
            var vehiculo = new Vehiculo
            {
                Id = GetIntValue(row, DB_COL_ID),
                Tipo = GetIntValue(row, DB_COL_TIPO),
                Combustible = GetIntValue(row, DB_COL_TIPO_COMBUSTIBLE),
                Modelo = GetIntValue(row, DB_COL_MODELO),
                Marca = GetIntValue(row, DB_COL_MARCA),
                Kilometraje = GetDoubleValue(row, DB_COL_KILOMETRAJE),
                cKmExcedido = GetDoubleValue(row, DB_COL_EXCEDIDO),
                cMalEstado = GetDoubleValue(row, DB_COL_MAL_ESTADO),
                Latitud = GetStringValue(row, DB_COL_LATITUD),
                Longitud = GetStringValue(row, DB_COL_LONGITUD),
                cLugarDiferente = GetDoubleValue(row, DB_COL_LUGAR_DIFF),
                Tarifa = GetDoubleValue(row, DB_COL_TARIFA),
                AccptInmediata = GetStringValue(row, DB_COL_ACEPT_INMD),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                Imagen = GetStringValue(row, DB_COL_IMAGEN),
                idUsuario = GetStringValue(row, DB_COL_USUARIO),
            };

            return vehiculo;
        }

    }
}
