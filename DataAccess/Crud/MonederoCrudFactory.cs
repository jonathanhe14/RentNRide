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
    public class MonederoCrudFactory : CrudFactory
    {
        MonederoMapper mapper;

        public MonederoCrudFactory() : base()
        {
            mapper = new MonederoMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var Monedero = (Monedero)entity;
            var sqlOperation = mapper.GetCreateStatement(Monedero);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var Monedero = (Monedero)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(Monedero));
        }

        public List<T> RetrieveTodo<T>(BaseEntity entity)
        {
            var listMonederos = new List<T>();
            var sqlOperation = mapper.GetRetriveStatement(entity);
            var lstResult = dao.ExecuteQueryProcedure(sqlOperation);
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    listMonederos.Add((T)Convert.ChangeType(c, typeof(T)));
                }

            }

            return listMonederos;


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
            var lstMonederos = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstMonederos.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstMonederos;
        }

        public override void Update(BaseEntity entity)
        {
            var Monedero = (Monedero)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(Monedero));
        }
    }
}
