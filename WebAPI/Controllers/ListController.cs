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
    public class ListController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        // GET api/list/5
        public IHttpActionResult Get(string id)
        {
            try
            {
                var mng = ListManager.GetInstance();
                //var option = new OptionList

                var lstOptions = mng.RetrieveById(id);
                return Ok(lstOptions);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        /*
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new VehiculoManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }
        */



    }
}