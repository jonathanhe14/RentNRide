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
    public class ReservaManager : BaseEntity
    {
        private ReservaCrudFactory crudReserva;


        public ReservaManager()
        {
            crudReserva = new ReservaCrudFactory();
        }

        public void Create(Reserva reserva)
        {
            try
            {
                crudReserva.Create(reserva);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Reserva> RetrieveAll()
        {
            try
            {
                return crudReserva.RetrieveAll<Reserva>();
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }

        }

        public Reserva RetrieveById(Reserva reserva)
        {
            try
            {
                return crudReserva.Retrieve<Reserva>(reserva);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }

        public List<Reserva> RetrieveAllById(Reserva reserva)
        {
            try
            {
                return crudReserva.RetrieveByUser<Reserva>(reserva);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }

        public void Update(Reserva reserva)
        {
            try
            {
                crudReserva.Update(reserva);
            }
            catch (Exception ex)
            {
                throw new BussinessException(0);
            }
        }

        public void Delete(Reserva reserva)
        {
            try
            {
                crudReserva.Delete(reserva);
            }
            catch (Exception ex)
            {

                throw new BussinessException(0);

            }
        }


    }
}
