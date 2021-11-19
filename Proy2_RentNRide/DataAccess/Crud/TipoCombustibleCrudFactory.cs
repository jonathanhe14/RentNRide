using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities_POJO;

namespace DataAccess.Crud {
    public class TipoCombustibleCrudFactory : CrudFactory {
        TipoCombustibleMapper mapper;

        public TipoCombustibleCrudFactory() : base() {
            mapper = new TipoCombustibleMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity) {
            var TipoCombustible = (TipoCombustible) entity;
            var sqlOperation = mapper.GetCreateStatement(TipoCombustible);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity) {
            var TipoCombustible = (TipoCombustible) entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(TipoCombustible));
        }

        public List<T> RetrieveTodo<T>(BaseEntity entity) {
            var listTipoCombustibles = new List<T>();
            var sqlOperation = mapper.GetRetriveStatement(entity);
            var lstResult = dao.ExecuteQueryProcedure(sqlOperation);
            var dic = new Dictionary<string, object>();
            if(lstResult.Count > 0) {
                var objs = mapper.BuildObjects(lstResult);
                foreach(var c in objs) {
                    listTipoCombustibles.Add((T) Convert.ChangeType(c, typeof(T)));
                }

            }

            return listTipoCombustibles;


        }

        public override T Retrieve<T>(BaseEntity entity) {
            var sqlOperation = mapper.GetRetriveStatement(entity);
            var lstResult = dao.ExecuteQueryProcedure(sqlOperation);
            var dic = new Dictionary<string, object>();
            if(lstResult.Count > 0) {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T) Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>() {
            var lstTipoCombustibles = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if(lstResult.Count > 0) {
                var objs = mapper.BuildObjects(lstResult);
                foreach(var c in objs) {
                    lstTipoCombustibles.Add((T) Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstTipoCombustibles;
        }

        public override void Update(BaseEntity entity) {
            var TipoCombustible = (TipoCombustible) entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(TipoCombustible));
        }
    }
}
