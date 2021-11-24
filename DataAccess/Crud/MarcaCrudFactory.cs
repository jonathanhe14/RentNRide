using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities_POJO;

namespace DataAccess.Crud {
    public class MarcaCrudFactory : CrudFactory {
        MarcaVehiMapper mapper;

        public MarcaCrudFactory() : base() {
            mapper = new MarcaVehiMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity) {
            var Marca = (Marca) entity;
            var sqlOperation = mapper.GetCreateStatement(Marca);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity) {
            var Marca = (Marca) entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(Marca));
        }

        public List<T> RetrieveTodo<T>(BaseEntity entity) {
            var listMarcas = new List<T>();
            var sqlOperation = mapper.GetRetriveStatement(entity);
            var lstResult = dao.ExecuteQueryProcedure(sqlOperation);
            var dic = new Dictionary<string, object>();
            if(lstResult.Count > 0) {
                var objs = mapper.BuildObjects(lstResult);
                foreach(var c in objs) {
                    listMarcas.Add((T) Convert.ChangeType(c, typeof(T)));
                }

            }

            return listMarcas;


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
            var lstMarcas = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if(lstResult.Count > 0) {
                var objs = mapper.BuildObjects(lstResult);
                foreach(var c in objs) {
                    lstMarcas.Add((T) Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstMarcas;
        }

        public override void Update(BaseEntity entity) {
            var Marca = (Marca) entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(Marca));
        }
    }
}
