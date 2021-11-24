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
    public class UserProfileCrudFactory : CrudFactory
    {
        UserProfileMapper mapper;
        public UserProfileCrudFactory() : base()
        {
            mapper = new UserProfileMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var usuarios = (UserProfile)entity;
            var sqlOperation = mapper.GetCreateStatement(usuarios);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
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
            throw new NotImplementedException();
        }

        public override void Update(BaseEntity entity)
        {
            var usuarios = (UserProfile)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(usuarios));
        }
    }
}
