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
    public class VehiculoManager
    {

        private VehiculoCrudFactory crudVehiculo;

        public VehiculoManager()
        {
            crudVehiculo = new VehiculoCrudFactory();
        }

        public void Create(Vehiculo vehiculo)
        {
            try
            {
                var c = crudVehiculo.Retrieve<Vehiculo>(vehiculo);

                if (c != null)
                {
                    //Customer already exist
                    throw new BussinessException(45);
                }

                /*
                if (vehiculo.Age >= 18)
                    crudVehiculo.Create(vehiculo);
                else
                    throw new BussinessException(2);
                */
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Vehiculo> RetrieveAll()
        {
            return crudVehiculo.RetrieveAll<Vehiculo>();
        }

        public Vehiculo RetrieveById(Vehiculo vehiculo)
        {
            Vehiculo c = null;
            try
            {
                c = crudVehiculo.Retrieve<Vehiculo>(vehiculo);
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

        public void Update(Vehiculo vehiculo)
        {
            crudVehiculo.Update(vehiculo);
        }

        public void Delete(Vehiculo vehiculo)
        {
            crudVehiculo.Delete(vehiculo);
        }

    }
}
