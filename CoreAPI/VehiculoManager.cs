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

                //if (c != null)
                //{
                //    //randomly generated id already exists, try again
                //    // dont need throw new BussinessException(45);
                //}

               
                    crudVehiculo.Create(vehiculo);
                
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

        public Boolean checkIfExists(Vehiculo vehiculo)
        {
            Vehiculo c = null;
            try
            {
                c = crudVehiculo.Retrieve<Vehiculo>(vehiculo);
                if (c == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
            return false;
        }
        public List<Vehiculo> RetrieveByEmail(Vehiculo vehiculo)
        {
            try
            {
                return crudVehiculo.TraerVehiEmail<Vehiculo>(vehiculo);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
                throw new BussinessException(0);
            }
        }
        public int GetAmount(Vehiculo vehiculo)
        {
            try
            {
                int beef = crudVehiculo.TraerVehiEmail<Vehiculo>(vehiculo).Count;
                return beef;
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
                throw new BussinessException(0);
            }
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
