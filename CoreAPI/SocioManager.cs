using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Crud;
using Entities_POJO;



namespace CoreAPI {
    public class SocioManager:BaseManager {

        private MembresiaCrudFactory crudMembresia;
        private ModeloCrudFactory crudModelo;
        private MarcaCrudFactory crudMarca;
        private TipoCombustibleCrudFactory crudTipoCombustible;
        private TipoVehiculoCrudFactory crudTipoVehiculo;

        private UsuariosCrudFactory crudUsuarios;
        public SocioManager() {
            crudMembresia = new MembresiaCrudFactory();
            crudModelo = new ModeloCrudFactory();
            crudMarca = new MarcaCrudFactory();
            crudTipoCombustible = new TipoCombustibleCrudFactory();
            crudTipoVehiculo = new TipoVehiculoCrudFactory();

            crudUsuarios = new UsuariosCrudFactory();
        }

        
        public Membresias RetrieveByIdMembresia(Usuarios userEm) {
            Membresias m = null;
            try {
                var usr = crudUsuarios.Retrieve<Usuarios>(userEm);
                Membresias membresia = crudMembresia.RetrieveByUser<Membresias>(usr);
                m = crudMembresia.Retrieve<Membresias>(membresia);
                if(m == null) {
                    throw new BussinessException(4);
                }
            } catch(Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }

            return m;
        }


        //Marcas


        public Marca RetrieveByIdMarca(Marca marca) {
            Marca m = null;
            try {
                m = crudMarca.Retrieve<Marca>(marca);
                if(m == null) {
                    throw new BussinessException(4);
                }
            } catch(Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }

            return m;
        }


        //Modelos


        public Modelo RetrieveByIdModelo(Modelo modelo) {
            Modelo m = null;
            try {
                m = crudModelo.Retrieve<Modelo>(modelo);
                if(m == null) {
                    throw new BussinessException(4);
                }
            } catch(Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }

            return m;
        }


        //Tipos Vehiculo


        public TipoVehiculo RetrieveByIdTipoVehiculo(TipoVehiculo tipoVehiculo) {
            TipoVehiculo m = null;
            try {
                m = crudTipoVehiculo.Retrieve<TipoVehiculo>(tipoVehiculo);
                if(m == null) {
                    throw new BussinessException(4);
                }
            } catch(Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }

            return m;
        }


        //Tipos de combustible


        public TipoCombustible RetrieveByIdTipoCombustible(TipoCombustible tipoCombustible) {
            TipoCombustible m = null;
            try {
                m = crudTipoCombustible.Retrieve<TipoCombustible>(tipoCombustible);
                if(m == null) {
                    throw new BussinessException(4);
                }
            } catch(Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }

            return m;
        }



    }
}
