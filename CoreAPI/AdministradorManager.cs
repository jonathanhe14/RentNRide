using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Crud;
using Entities_POJO;



namespace CoreAPI {
    public class AdministradorManager:BaseManager {

        private MembresiaCrudFactory crudMembresia;
        private ModeloCrudFactory crudModelo;
        private MarcaCrudFactory crudMarca;
        private TipoCombustibleCrudFactory crudTipoCombustible;
        private TipoVehiculoCrudFactory crudTipoVehiculo;
        private UsuariosCrudFactory crudUsuarios;

        public AdministradorManager() {
            crudMembresia = new MembresiaCrudFactory();
            crudModelo = new ModeloCrudFactory();
            crudMarca = new MarcaCrudFactory();
            crudTipoCombustible = new TipoCombustibleCrudFactory();
            crudTipoVehiculo = new TipoVehiculoCrudFactory();
            crudUsuarios = new UsuariosCrudFactory();
        }

        public void CreateMembresia(Membresias membresia) {
            try {
                var m = crudMembresia.Retrieve<Membresias>(membresia);

                if(m != null) {
                    //Membresias already exist
                    throw new BussinessException(45);
                }

                crudMembresia.Create(membresia);
            } catch(Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Membresias> RetrieveAllMembresias() {
            return crudMembresia.RetrieveAll<Membresias>();
        }

        public Membresias RetrieveByIdMembresia(Membresias membresia) {
            Membresias m = null;
            try {
                m = crudMembresia.Retrieve<Membresias>(membresia);
                if(m == null) {
                    throw new BussinessException(4);
                }
            } catch(Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }

            return m;
        }

        public void UpdateMembresia(Membresias membresia) {
            crudMembresia.Update(membresia);
        }

        public void DeleteMembresia(Membresias membresia) {
            crudMembresia.Delete(membresia);
        }

        //Marcas

        public void CreateMarca(Marca marca) {
            try {
                var m = crudMarca.Retrieve<Marca>(marca);

                if(m != null) {
                    //Marcas already exist
                    throw new BussinessException(45);
                }

                crudMarca.Create(marca);
            } catch(Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Marca> RetrieveAllMarcas() {
            return crudMarca.RetrieveAll<Marca>();
        }

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

        public void UpdateMarca(Marca marca) {
            crudMarca.Update(marca);
        }

        public void DeleteMarca(Marca marca) {
            crudMarca.Delete(marca);
        }

        //Modelos

        public void CreateModelo(Modelo modelo) {
            try {
                var m = crudModelo.Retrieve<Modelo>(modelo);

                if(m != null) {
                    //Modelos already exist
                    throw new BussinessException(45);
                }

                crudModelo.Create(modelo);
            } catch(Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Modelo> RetrieveAllModelos() {
            return crudModelo.RetrieveAll<Modelo>();
        }

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

        public void UpdateModelo(Modelo modelo) {
            crudModelo.Update(modelo);
        }

        public void DeleteModelo(Modelo modelo) {
            crudModelo.Delete(modelo);
        }

        //Tipos Vehiculo

        public void CreateTipoVehiculo(TipoVehiculo tipoVehiculo) {
            try {
                var m = crudTipoVehiculo.Retrieve<TipoVehiculo>(tipoVehiculo);

                if(m != null) {
                    //TipoVehiculos already exist
                    throw new BussinessException(45);
                }

                crudTipoVehiculo.Create(tipoVehiculo);
            } catch(Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<TipoVehiculo> RetrieveAllTipoVehiculos() {
            return crudTipoVehiculo.RetrieveAll<TipoVehiculo>();
        }

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

        public void UpdateTipoVehiculo(TipoVehiculo tipoVehiculo) {
            crudTipoVehiculo.Update(tipoVehiculo);
        }

        public void DeleteTipoVehiculo(TipoVehiculo tipoVehiculo) {
            crudTipoVehiculo.Delete(tipoVehiculo);
        }

        //Tipos de combustible


        public void CreateTipoCombustible(TipoCombustible tipoCombustible) {
            try {
                var m = crudTipoCombustible.Retrieve<TipoCombustible>(tipoCombustible);

                if(m != null) {
                    //TipoCombustibles already exist
                    throw new BussinessException(45);
                }

                crudTipoCombustible.Create(tipoCombustible);
            } catch(Exception ex) {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<TipoCombustible> RetrieveAllTipoCombustibles() {
            return crudTipoCombustible.RetrieveAll<TipoCombustible>();
        }

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

        public void UpdateTipoCombustible(TipoCombustible tipoCombustible) {
            crudTipoCombustible.Update(tipoCombustible);
        }

        public void DeleteTipoCombustible(TipoCombustible tipoCombustible) {
            crudTipoCombustible.Delete(tipoCombustible);
        }


        public async void AceptarSocio(int id, Usuarios usr) {
            usr = crudUsuarios.Retrieve<Usuarios>(usr);
            var mem = crudMembresia.Retrieve<Membresias>(new Membresias { Id = id });

            usr.Estado = "MEMBRESIA_ENVIADA";
            crudUsuarios.Update(usr);
            await NotificacionesManager.EnviarCorreoMembresia(usr, mem, "ACEPTADO");




        }

        public async void RechazarSocio(Usuarios usr) {
            usr = crudUsuarios.Retrieve<Usuarios>(usr);
            usr.Estado = "RECHAZADO";

            crudUsuarios.Update(usr);

            await NotificacionesManager.EnviarCorreoMembresia(usr, null, "RECHAZADO");
        }



    }
}
