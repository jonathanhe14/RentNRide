using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    class DocumentoMapper : EntityMapper, ISqlStatements, IObjectMapper
    {

        private const string DB_COL_ID = "P_id";
        private const string DB_COL_MARCHAMO = "P_marchamo";
        private const string DB_COL_TITULO_PROPIEDAD= "P_tituloProp";
        private const string DB_COL_RITEVE = "P_riteve";
        private const string DB_COL_DERECHO_CIRCULACION = "P_circulacion";
        

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_DOCUMENTO_PR" };

            var c = (Documento)entity;
            operation.AddIntParam(DB_COL_ID, c.idVehi);
            operation.AddVarcharParam(DB_COL_MARCHAMO, c.Marchamo);
            operation.AddVarcharParam(DB_COL_TITULO_PROPIEDAD, c.tituloPropiedad);
            operation.AddVarcharParam(DB_COL_RITEVE, c.Riteve);
            operation.AddVarcharParam(DB_COL_DERECHO_CIRCULACION, c.derechoCirculacion);
            

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_DOCUMENTO_PR" };

            var c = (Documento)entity;
            operation.AddIntParam(DB_COL_ID, c.idVehi);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_DOCUMENTO_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_DOCUMENTO_PR" };

            var c = (Documento)entity;
            operation.AddIntParam(DB_COL_ID, c.idVehi);
            operation.AddVarcharParam(DB_COL_MARCHAMO, c.Marchamo);
            operation.AddVarcharParam(DB_COL_TITULO_PROPIEDAD, c.tituloPropiedad);
            operation.AddVarcharParam(DB_COL_RITEVE, c.Riteve);
            operation.AddVarcharParam(DB_COL_DERECHO_CIRCULACION, c.derechoCirculacion);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_DOCUMENTO_PR" };

            var c = (Documento)entity;
            operation.AddIntParam(DB_COL_ID, c.idVehi);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var documento = BuildObject(row);
                lstResults.Add(documento);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var documento = new Documento
            {
                idVehi = GetIntValue(row, DB_COL_ID),
                Marchamo = GetStringValue(row, DB_COL_MARCHAMO),
                tituloPropiedad = GetStringValue(row, DB_COL_TITULO_PROPIEDAD),
                Riteve = GetStringValue(row, DB_COL_RITEVE),
                derechoCirculacion = GetStringValue(row, DB_COL_DERECHO_CIRCULACION),
                
            };

            return documento;
        }

    }
}
