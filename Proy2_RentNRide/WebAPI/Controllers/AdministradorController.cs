using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using Entities_POJO;
using CoreAPI;
using Exceptions;

namespace WebAPI.Controllers
{
    public class AdministradorController : ApiController {

        ApiResponse apiResp = new ApiResponse();

        // GET api/administrador
        //Membresias
        // Retrieve
        [HttpGet]
        public IHttpActionResult GetMembresias() {

            apiResp = new ApiResponse();
            var mng = new AdministradorManager();
            apiResp.Data = mng.RetrieveAllMembresias();
            apiResp.Message = "Membresias obtenidas";

            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult GetMembresia(int id) {

            try {
                var mng = new AdministradorManager();
                var membresia = new Membresias {
                    Id = id
                };

                membresia = mng.RetrieveByIdMembresia(membresia);
                apiResp = new ApiResponse();
                apiResp.Data = membresia;
                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult PostMembresia(Membresias membresia) {

            try {
                membresia.Activo = "ACTIVO";
                membresia.FechaCreacion = DateTime.Now;
                var mng = new AdministradorManager();
                mng.CreateMembresia(membresia);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Message));
            }
        }
        [HttpPut]
        public IHttpActionResult PutMembresia(Membresias membresia) {
            try {
                var mng = new AdministradorManager();
                mng.UpdateMembresia(membresia);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // DELETE ==
        [HttpDelete]
        public IHttpActionResult DeleteMembresia(int id) {
            try {
                var mng = new AdministradorManager();
                var membresia = new Membresias {
                    Id = id
                };
                mng.DeleteMembresia(membresia);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        //Marcas
        [HttpGet]
        public IHttpActionResult GetMarcas() {

            apiResp = new ApiResponse();
            var mng = new AdministradorManager();
            apiResp.Data = mng.RetrieveAllMarcas();
            apiResp.Message = "Marcas obtenidas";

            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult GetMarca(int id) {

            try {
                var mng = new AdministradorManager();
                var marca = new Marca {
                    Id = id
                };

                marca = mng.RetrieveByIdMarca(marca);
                apiResp = new ApiResponse();
                apiResp.Data = marca;
                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult PostMarca(Marca marca) {

            try {
                var mng = new AdministradorManager();
                marca.Estado = "ACTIVO";
                mng.CreateMarca(marca);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Message));
            }
        }
        [HttpPut]
        public IHttpActionResult PutMarca(Marca marca) {
            try {
                var mng = new AdministradorManager();
                mng.UpdateMarca(marca);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // DELETE ==
        [HttpDelete]
        public IHttpActionResult DeleteMarca(int id) {
            try {
                var mng = new AdministradorManager();
                var marca = new Marca {
                    Id = id
                };
                mng.DeleteMarca(marca);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        //Modelos
        [HttpGet]
        public IHttpActionResult GetModelos() {

            apiResp = new ApiResponse();
            var mng = new AdministradorManager();
            apiResp.Data = mng.RetrieveAllModelos();
            apiResp.Message = "Modelos obtenidas";

            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult GetModelo(int id) {

            try {
                var mng = new AdministradorManager();
                var modelo = new Modelo {
                    Id = id
                };

                modelo = mng.RetrieveByIdModelo(modelo);
                apiResp = new ApiResponse();
                apiResp.Data = modelo;
                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult PostModelo(Modelo modelo) {

            try {
                var mng = new AdministradorManager();
                modelo.Estado = "ACTIVO";
                mng.CreateModelo(modelo);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Message));
            }
        }
        [HttpPut]
        public IHttpActionResult PutModelo(Modelo modelo) {
            try {
                var mng = new AdministradorManager();
                mng.UpdateModelo(modelo);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // DELETE ==
        [HttpDelete]
        public IHttpActionResult DeleteModelo(int id) {
            try {
                var mng = new AdministradorManager();
                var modelo = new Modelo {
                    Id = id
                };
                mng.DeleteModelo(modelo);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        //Tipos de Vehiculo
        [HttpGet]
        public IHttpActionResult GetTipoVehiculos() {

            apiResp = new ApiResponse();
            var mng = new AdministradorManager();
            apiResp.Data = mng.RetrieveAllTipoVehiculos();
            apiResp.Message = "TipoVehiculos obtenidas";

            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult GetTipoVehiculo(int id) {

            try {
                var mng = new AdministradorManager();
                var tipoVehiculo = new TipoVehiculo {
                    Id = id
                };

                tipoVehiculo = mng.RetrieveByIdTipoVehiculo(tipoVehiculo);
                apiResp = new ApiResponse();
                apiResp.Data = tipoVehiculo;
                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult PostTipoVehiculo(TipoVehiculo tipoVehiculo) {

            try {
                var mng = new AdministradorManager();
                tipoVehiculo.Estado = "ACTIVO";
                mng.CreateTipoVehiculo(tipoVehiculo);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Message));
            }
        }
        [HttpPut]
        public IHttpActionResult PutTipoVehiculo(TipoVehiculo tipoVehiculo) {
            try {
                var mng = new AdministradorManager();
                mng.UpdateTipoVehiculo(tipoVehiculo);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // DELETE ==
        [HttpDelete]
        public IHttpActionResult DeleteTipoVehiculo(int id) {
            try {
                var mng = new AdministradorManager();
                var tipoVehiculo = new TipoVehiculo {
                    Id = id
                };
                mng.DeleteTipoVehiculo(tipoVehiculo);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        //Tipos de combustible
        [HttpGet]
        public IHttpActionResult GetTipoCombustibles() {

            apiResp = new ApiResponse();
            var mng = new AdministradorManager();
            apiResp.Data = mng.RetrieveAllTipoCombustibles();
            apiResp.Message = "TipoCombustibles obtenidas";

            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult GetTipoCombustible(int id) {

            try {
                var mng = new AdministradorManager();
                var tipoCombustible = new TipoCombustible {
                    Id = id
                };

                tipoCombustible = mng.RetrieveByIdTipoCombustible(tipoCombustible);
                apiResp = new ApiResponse();
                apiResp.Data = tipoCombustible;
                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult PostTipoCombustible(TipoCombustible tipoCombustible) {

            try {
                var mng = new AdministradorManager();
                tipoCombustible.Estado = "ACTIVO";
                mng.CreateTipoCombustible(tipoCombustible);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Message));
            }
        }
        [HttpPut]
        public IHttpActionResult PutTipoCombustible(TipoCombustible tipoCombustible) {
            try {
                var mng = new AdministradorManager();
                mng.UpdateTipoCombustible(tipoCombustible);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // DELETE ==
        [HttpDelete]
        public IHttpActionResult DeleteTipoCombustible(int id) {
            try {
                var mng = new AdministradorManager();
                var tipoCombustible = new TipoCombustible {
                    Id = id
                };
                mng.DeleteTipoCombustible(tipoCombustible);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult Rechazar(Usuarios usuario) {
            apiResp = new ApiResponse();
            var mng = new AdministradorManager();
            mng.RechazarSocio(usuario);
            apiResp.Message = "Socio rechazado";

            return Ok(apiResp);
        }


        [HttpPost]
        public IHttpActionResult Aceptar(int id, Usuarios usuario) {
            apiResp = new ApiResponse();
            var mng = new AdministradorManager();
            mng.AceptarSocio(id, usuario);
            apiResp.Message = "Membresia enviada";

            return Ok(apiResp);
        }




    }
}
