using DataAccess.Crud;
using Entities_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class HorarioManager
    {

        private HorarioCrudFactory crudHorario;

        public HorarioManager()
        {
            crudHorario = new HorarioCrudFactory();
        }

        public void Create(Horario horario)
        {
            try
            {
               

                
                    crudHorario.Create(horario);
                
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Horario> RetrieveAll()
        {
            return crudHorario.RetrieveAll<Horario>();
        }

        public Horario RetrieveById(Horario horario)
        {
            Horario c = null;
            try
            {
                c = crudHorario.Retrieve<Horario>(horario);
                if (c == null)
                {
                    throw new BussinessException(4);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return c;
        }

        public void Update(Horario horario)
        {
            crudHorario.Update(horario);
        }

        public void Delete(Horario horario)
        {
            crudHorario.Delete(horario);
        }

    }
}
