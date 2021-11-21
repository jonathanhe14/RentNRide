using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DataAccess.Mapper
{
    public class UsuariosMapper : EntityMapper, ISqlStatements, IObjectMapper

    {
        private const string DB_COL_id_usuario = "ID_USUARIO";
        private const string DB_COL_cedula = "CEDULA";
        private const string DB_COL_nombre = "NOMBRE";
        private const string DB_COL_apellidos = "APELLIDOS";
        private const string DB_COL_correo = "CORREO";
        private const string DB_COL_fecha_nacimiento = "FECHA_NACIMIENTO";
        private const string DB_COL_edad = "EDAD";
        private const string DB_COL_telefono = "TELEFONO";
        private const string DB_COL_latitud = "LATITUD";
        private const string DB_COL_longitud = "LONGITUD";
        private const string DB_COL_personeria_juridica = "PERSONERIA_JURIDICA";
        private const string DB_COL_permiso_operaciones = "PERMISO_OPERACIONES";
        private const string DB_COL_contrasenna_actual = "CONTRASENNA_ACTUAL";
        private const string DB_COL_activo = "ACTIVO";
        private const string DB_COL_otp = "OTP";
        private const string DB_COL_comprobacion = "COMPROBACION";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_USUARIO_PR" };

            var u = (Usuarios)entity;
            operation.AddVarcharParam(DB_COL_cedula, u.Cedula);
            operation.AddVarcharParam(DB_COL_nombre, u.Nombre);
            operation.AddVarcharParam(DB_COL_apellidos, u.Apellidos);
            operation.AddVarcharParam(DB_COL_correo, u.Correo);
            operation.AddDateTimeParam(DB_COL_fecha_nacimiento, u.FechaNacimiento);
            operation.AddIntParam(DB_COL_edad, u.Edad);
            operation.AddVarcharParam(DB_COL_telefono, u.Telefono);
            operation.AddVarcharParam(DB_COL_latitud, u.Latitud);
            operation.AddVarcharParam(DB_COL_longitud, u.Longitud);
            operation.AddVarcharParam(DB_COL_personeria_juridica, u.PersoneriaJuridica);
            operation.AddVarcharParam(DB_COL_permiso_operaciones, u.PermisoOperaciones);
            operation.AddVarcharParam(DB_COL_contrasenna_actual, u.ContrassenaActual);
            operation.AddIntParam(DB_COL_otp, u.OTP);
            operation.AddVarcharParam(DB_COL_activo, u.Estado);
            operation.AddVarcharParam(DB_COL_comprobacion, u.Comprobacion);           


            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_USUARIOS_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_USUARIOS_PR" };

            var c = (Usuarios)entity;
            operation.AddVarcharParam(DB_COL_correo, c.Correo);
            operation.AddVarcharParam(DB_COL_telefono, c.Telefono);

            return operation;
        }
        public SqlOperation VerificarUsuario(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_VERIFICAR_USUARIO_PR" };

            var c = (Usuarios)entity;
            operation.AddVarcharParam(DB_COL_correo, c.Correo);
            operation.AddVarcharParam(DB_COL_cedula, c.Cedula);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_USUARIO_PR" };

            var u = (Usuarios)entity;
            operation.AddIntParam(DB_COL_id_usuario, u.Id_usuario);
            operation.AddVarcharParam(DB_COL_cedula, u.Cedula);
            operation.AddVarcharParam(DB_COL_nombre, u.Nombre);
            operation.AddVarcharParam(DB_COL_apellidos, u.Apellidos);
            operation.AddVarcharParam(DB_COL_correo, u.Correo);
            operation.AddDateTimeParam(DB_COL_fecha_nacimiento, u.FechaNacimiento);
            operation.AddIntParam(DB_COL_edad, u.Edad);
            operation.AddVarcharParam(DB_COL_telefono, u.Telefono);
            operation.AddVarcharParam(DB_COL_latitud, u.Latitud);
            operation.AddVarcharParam(DB_COL_longitud, u.Longitud);
            operation.AddVarcharParam(DB_COL_personeria_juridica, u.PersoneriaJuridica);
            operation.AddVarcharParam(DB_COL_permiso_operaciones, u.PermisoOperaciones);
            operation.AddVarcharParam(DB_COL_contrasenna_actual, u.ContrassenaActual);
            operation.AddIntParam(DB_COL_otp, u.OTP);
            operation.AddVarcharParam(DB_COL_comprobacion, u.Comprobacion);
            operation.AddVarcharParam(DB_COL_activo, u.Estado);



            return operation;
        }
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            try
            {
                var usuario = new Usuarios
                {
                    Id_usuario = GetIntValue(row, DB_COL_id_usuario),
                    Cedula = GetStringValue(row, DB_COL_cedula),
                    Nombre = GetStringValue(row, DB_COL_nombre),
                    Apellidos = GetStringValue(row, DB_COL_apellidos),
                    Correo = GetStringValue(row, DB_COL_correo),
                    FechaNacimiento = GetDateValue(row, DB_COL_fecha_nacimiento),
                    Edad = GetIntValue(row, DB_COL_edad),
                    Telefono = GetStringValue(row, DB_COL_telefono),
                    Latitud = GetStringValue(row, DB_COL_latitud),
                    Longitud = GetStringValue(row, DB_COL_longitud),
                    PersoneriaJuridica = GetStringValue(row, DB_COL_personeria_juridica),
                    PermisoOperaciones = GetStringValue(row, DB_COL_permiso_operaciones),
                    ContrassenaActual = GetStringValue(row, DB_COL_contrasenna_actual),
                    Estado = GetStringValue(row, DB_COL_activo),
                    OTP = GetIntValue(row, DB_COL_otp),
                    Comprobacion = GetStringValue(row, DB_COL_comprobacion)
                };
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }




        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {

                var usuarios = BuildObject(row);
                lstResults.Add(usuarios);
            }

            return lstResults;
        }
    }
}