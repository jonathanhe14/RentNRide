using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities_POJO;

namespace DataAccess.Crud {
    public class MembresiaCrudFactory : CrudFactory {
        MembresiaMapper mapper;

        public MembresiaCrudFactory() : base() {
            mapper = new MembresiaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity) {
            var Membresia = (Membresias) entity;
            var sqlOperation = mapper.GetCreateStatement(Membresia);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity) {
            var Membresia = (Membresias) entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(Membresia));
        }

        public List<T> RetrieveTodo<T>(BaseEntity entity) {
            var listMembresias = new List<T>();
            var sqlOperation = mapper.GetRetriveStatement(entity);
            var lstResult = dao.ExecuteQueryProcedure(sqlOperation);
            var dic = new Dictionary<string, object>();
            if(lstResult.Count > 0) {
                var objs = mapper.BuildObjects(lstResult);
                foreach(var c in objs) {
                    listMembresias.Add((T) Convert.ChangeType(c, typeof(T)));
                }

            }

            return listMembresias;


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
            var lstMembresias = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if(lstResult.Count > 0) {
                var objs = mapper.BuildObjects(lstResult);
                foreach(var c in objs) {
                    lstMembresias.Add((T) Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstMembresias;
        }

        public override void Update(BaseEntity entity) {
            var Membresia = (Membresias) entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(Membresia));
        }
    }
}
