using DataAccess.Dao;
using DataAccess.Mapper;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class ContrasennaCrudFactory : CrudFactory
    {
        ContrasennaMapper mapper;

        public ContrasennaCrudFactory() : base()
        {
            mapper = new ContrasennaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var Contrasenna = (Contrasennas)entity;
            var sqlOperation = mapper.GetCreateStatement(Contrasenna);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var Contrasenna = (Contrasennas)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(Contrasenna));
        }

        public List<T> RetrieveTodo<T>(BaseEntity entity)
        {
            var listContrasennas = new List<T>();
            var sqlOperation = mapper.GetRetriveStatement(entity);
            var lstResult = dao.ExecuteQueryProcedure(sqlOperation);
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    listContrasennas.Add((T)Convert.ChangeType(c, typeof(T)));
                }

            }

            return listContrasennas;


        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var sqlOperation = mapper.GetRetriveStatement(entity);
            var lstResult = dao.ExecuteQueryProcedure(sqlOperation);
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstContrasennas = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstContrasennas.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstContrasennas;
        }

        public override void Update(BaseEntity entity)
        {
            var Contrasenna = (Contrasennas)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(Contrasenna));
        }
    }
}
