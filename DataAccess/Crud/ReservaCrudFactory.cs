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
    public class ReservaCrudFactory : CrudFactory
    {

        ReservaMapper mapper;
        public ReservaCrudFactory() : base()
        {
            mapper = new ReservaMapper();
            dao = SqlDao.GetInstance();
        }
        public override void Create(BaseEntity entity)
        {
            var customer = (Reserva)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (Reserva)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
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

        public T RetrieveReserva<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatementById(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public List<T> RetrieveByUser<T>(BaseEntity entity)
        {
            var lstHorarios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstHorarios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstHorarios;
        }
        public List<T> RetrieveBySocio<T>(BaseEntity entity)
        {
            var lstHorarios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatementSocio(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstHorarios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstHorarios;
        }
        public List<T> RetrieveByPendientes<T>(BaseEntity entity)
        {
            var lstHorarios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatementPendientes(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstHorarios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstHorarios;
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstHorarios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstHorarios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstHorarios;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (Reserva)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public List<T> RetrieveDisponibility<T>(BaseEntity entity)
        {
            var lstHorarios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetDisponibilidad(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjectsConsulta(lstResult);
                foreach (var c in objs)
                {
                    lstHorarios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstHorarios;
        }
        public void UpdateEstado(BaseEntity entity)
        {
            var customer = (Reserva)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatementEstado(customer));
        }
    }
}
