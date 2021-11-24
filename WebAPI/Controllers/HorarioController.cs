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
    public class HorarioController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        // GET api/vehiculo
        // Retrieve
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new HorarioManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        // GET api/
        // Retrieve by id
        public IHttpActionResult Get(int id)
        {
            try
            {
                var mng = new HorarioManager();
                var customer = new Horario
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

        // POST 
        // CREATE
        public IHttpActionResult Post(Horario horario)
        {

            try
            {
                var mng = new HorarioManager();
                mng.Create(horario);

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
        public IHttpActionResult Put(Horario horario)
        {
            try
            {
                var mng = new HorarioManager();
                mng.Update(horario);

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
        public IHttpActionResult Delete(Horario horario)
        {
            try
            {
                var mng = new HorarioManager();
                mng.Delete(horario);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

    }
}