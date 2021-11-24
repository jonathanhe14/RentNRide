using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities_POJO;

namespace DataAccess.Crud {
    public class ModeloCrudFactory : CrudFactory {
        ModeloMapper mapper;

        public ModeloCrudFactory() : base() {
            mapper = new ModeloMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity) {
            var Modelo = (Modelo) entity;
            var sqlOperation = mapper.GetCreateStatement(Modelo);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity) {
            var Modelo = (Modelo) entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(Modelo));
        }

        public List<T> RetrieveTodo<T>(BaseEntity entity) {
            var listModelos = new List<T>();
            var sqlOperation = mapper.GetRetriveStatement(entity);
            var lstResult = dao.ExecuteQueryProcedure(sqlOperation);
            var dic = new Dictionary<string, object>();
            if(lstResult.Count > 0) {
                var objs = mapper.BuildObjects(lstResult);
                foreach(var c in objs) {
                    listModelos.Add((T) Convert.ChangeType(c, typeof(T)));
                }

            }

            return listModelos;


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
            var lstModelos = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if(lstResult.Count > 0) {
                var objs = mapper.BuildObjects(lstResult);
                foreach(var c in objs) {
                    lstModelos.Add((T) Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstModelos;
        }

        public override void Update(BaseEntity entity) {
            var Modelo = (Modelo) entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(Modelo));
        }
    }
}
