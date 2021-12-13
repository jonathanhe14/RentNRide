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
    public class SocioController : ApiController {

        ApiResponse apiResp = new ApiResponse();


        // GET api/socio
        //Membresias
        // Retrieve
        [HttpGet]
        public IHttpActionResult GetMembresiaSocio(string correo) {

            try {
                var mng = new SocioManager();
                var usuario = new Usuarios {
                    Correo = id
                };
                var membresia = new Membresias();
                membresia = mng.RetrieveByIdMembresia(usuario);
                apiResp = new ApiResponse();
                apiResp.Data = membresia;
                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
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
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
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
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
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
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
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
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

    }
}
