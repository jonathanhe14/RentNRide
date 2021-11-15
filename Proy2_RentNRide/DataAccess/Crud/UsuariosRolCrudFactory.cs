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
    public class UsuariosRolCrudFactory : CrudFactory
    {
        UsuariosRolMapper mapper;

        public UsuariosRolCrudFactory() : base()
        {
            mapper = new UsuariosRolMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var usuarios = (Usuarios)entity;
            var sqlOperation = mapper.GetCreateStatement(usuarios);
            dao.ExecuteProcedure(sqlOperation);
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
            var lstUsuariosRoles = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstUsuariosRoles.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstUsuariosRoles;
        }

        public override void Update(BaseEntity entity)
        {
            var usuarios = (Usuarios)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(usuarios));
        }

        public override void Delete(BaseEntity entity)
        {
            var usuarios = (Usuarios)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(usuarios));
        }
    }
}
