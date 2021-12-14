using DataAccess.Dao;
using DataAccess.Mapper;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Crud
{
    public class UsuariosCrudFactory : CrudFactory
    {
        UsuariosMapper mapper;

        public UsuariosCrudFactory() : base()
        {
            mapper = new UsuariosMapper();
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



        public T VerificarUsuario<T>(BaseEntity entity)
        {
            var sqlOperation = mapper.VerificarUsuario(entity);
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

        public void UpdateState(BaseEntity entity) {
            var usuarios = (Usuarios) entity;
            dao.ExecuteProcedure(mapper.GetUpdateStateStatement(usuarios));
        }
        public T PerfilUsuario<T>(BaseEntity entity)
        {
            var sqlOperation = mapper.UsuarioPerfil(entity);
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
            var lstUsuarios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstUsuarios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstUsuarios;
        }

        public List<T> TraerPerfil<T>(BaseEntity entity)
        {
            var lstUsuarios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.UsuarioPerfil(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstUsuarios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstUsuarios;
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
        public List<T> RetrieveAllSolicitudes<T>() {
            var lstUsuarios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllSolicitudesStatement());
            var dic = new Dictionary<string, object>();
            if(lstResult.Count > 0) {
                var objs = mapper.BuildObjects(lstResult);
                foreach(var c in objs) {
                    lstUsuarios.Add((T) Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstUsuarios;
        }

    }
}
