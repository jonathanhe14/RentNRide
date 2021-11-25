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
    public class DocumentoController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        // GET api/vehiculo
        // Retrieve
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new DocumentoManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        // GET api/
        // Retrieve by id
        public IHttpActionResult Get(int id)
        {
            try
            {
                var mng = new DocumentoManager();
                var customer = new Documento
                {
                    idVehi = id
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
        public IHttpActionResult Post(Documento documento)
        {

            try
            {
                var mng = new DocumentoManager();
                mng.Create(documento);

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
        public IHttpActionResult Put(Documento documento)
        {
            try
            {
                var mng = new DocumentoManager();
                mng.Update(documento);

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
        public IHttpActionResult Delete(Documento documento)
        {
            try
            {
                var mng = new DocumentoManager();
                mng.Delete(documento);

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