using DataAccess.Dao;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class UserProfileMapper : EntityMapper, ISqlStatements, IObjectMapper
    {
        private const string DB_COL_USER_ID = "USER_ID";
        private const string DB_COL_USER_NAME = "USER_NAME";
        private const string DB_COL_PASSWORD = "PASSWORD";
        private const string DB_COL_FULL_NAME = "FULL_NAME";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var user = new UserProfile
            {
                UserId = GetStringValue(row, DB_COL_USER_ID),
                UserName = GetStringValue(row, DB_COL_USER_NAME),
                FullName = GetStringValue(row, DB_COL_FULL_NAME),
            };

            return user;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var user = BuildObject(row);
                lstResults.Add(user);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_USER_PROFILE_PR" };

            var u = (UserProfile)entity;
            operation.AddVarcharParam(DB_COL_USER_NAME, u.UserName);
            operation.AddVarcharParam(DB_COL_PASSWORD, u.Password);
            operation.AddVarcharParam(DB_COL_FULL_NAME, u.FullName);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_USER_PROFILE_PR" };

            var u = (UserProfile)entity;
            operation.AddVarcharParam(DB_COL_USER_NAME, u.UserName);
            operation.AddVarcharParam(DB_COL_PASSWORD, u.Password);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_USER_PROFILE_PR" };

            var u = (UserProfile)entity;
            operation.AddVarcharParam(DB_COL_USER_NAME, u.UserName);
            operation.AddVarcharParam(DB_COL_PASSWORD, u.Password);

            return operation;
        }
    }
}
