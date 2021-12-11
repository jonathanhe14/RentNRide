using CoreAPI;
using Entities_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class VehiculoController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        // GET api/vehiculo
        // Retrieve
        ListController listi = new ListController();
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new VehiculoManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        // GET api/
        // Retrieve by id
        public IHttpActionResult Get(int id)
        {
            try
            {
                var mng = new VehiculoManager();
                var customer = new Vehiculo
                {
                    Id = id
                };

                customer = mng.RetrieveById(customer);
                apiResp = new ApiResponse();
                apiResp.Data = customer;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        [HttpGet]
        public IHttpActionResult GetOneVehicle(int id)
        {
            try
            {
                var mng = new VehiculoManager();
                var customer = new Vehiculo
                {
                    Id = id
                };

                customer = mng.RetrieveById(customer);
                apiResp = new ApiResponse();
                apiResp.Data = customer;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        public IHttpActionResult GetCheck(int id)
        {
            try
            {
                var mng = new VehiculoManager();
                var customer = new Vehiculo
                {
                    Id = id
                };

                var doesItExi = mng.checkIfExists(customer);
                apiResp = new ApiResponse();
                apiResp.Data = doesItExi;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }
        // POST 
        // CREATE
        public IHttpActionResult Post(Vehiculo vehiculo)
        {

            try
            {
                var mng = new VehiculoManager();


                mng.Create(vehiculo);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Mensaje));
            }
        }

        // PUT
        // UPDATE
        public IHttpActionResult Put(Vehiculo vehiculo)
        {
            try
            {
                var mng = new VehiculoManager();
                mng.Update(vehiculo);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        // DELETE ==
        public IHttpActionResult Delete(Vehiculo vehiculo)
        {
            try
            {
                var mng = new VehiculoManager();
                mng.Delete(vehiculo);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        public IHttpActionResult GetV(string correo)
        {
            try
            {
                var mng = new VehiculoManager();
                var customer = new Vehiculo
                {
                    idUsuario = correo
                };
                apiResp = new ApiResponse();
                List<Object> trueVehi = new List<Object>();
                var vvehiculo = mng.RetrieveByEmail(customer);
                /*
                var tipo = listi.GetTableSpace("LST_tipoVehi");
                var combust = listi.GetTableSpace("LST_tipoCombu");
                var marca = listi.GetTableSpace("LST_tipoMarca");
                var modelo = listi.GetTableSpace("LST_tipoModelo");
                trueVehi = (T)Convert.ChangeType(vvehiculo, typeof(T));
                foreach (var vehicul in vvehiculo)
                {
                    var currTyp = vehicul.Tipo;
                    foreach (var typ in tipo)
                    {
                        if (currTyp == typ.id)
                        {
                            vehicul.Tipo = typ.nombre;
                        }
                    }
                }
                */
                apiResp.Data = vvehiculo;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }
        public IHttpActionResult GetData(string correo)
        {
            try
            {
                var mng = new VehiculoManager();
                var customer = new Vehiculo
                {
                    idUsuario = correo
                };
                apiResp = new ApiResponse();
                apiResp.Data = mng.GetAmount(customer);

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllVehicles()
        {
            try
            {
                var mng = new VehiculoManager();
                apiResp = new ApiResponse();

                apiResp.Data = mng.RetrieveAll();
                apiResp.Message = "success";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }
    }
}