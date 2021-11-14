using CoreAPI;
using Entities_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UserProfileController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        [HttpPost]
        public IHttpActionResult InicioSesion(UserProfile user)
        {
            var mng = new UserProfileManager();
            user = mng.ValidateUser(user);
            apiResp = new ApiResponse();
            apiResp.Data = user;
            return Ok(apiResp);

            /*try
            {
                var mng = new UserProfileManager();
                user = mng.ValidateUser(user);
                apiResp = new ApiResponse();
                apiResp.Data = user;
                return Ok(apiResp);
                
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }*/
        }

        [HttpPost]
        public IHttpActionResult RecuperarClaveCorreo(UserProfile user)
        {
            var mng = new NotificacionesManager();
            string respuesta = mng.recuperarClaveCorreo(user);
            apiResp = new ApiResponse();
            apiResp.Message = "Correo enviado";
            apiResp.Data = user;
            return Ok(apiResp);
        }

        [HttpPost]
        public IHttpActionResult RecuperarClaveSMS(UserProfile user)
        {
            var mng = new NotificacionesManager();
            string respuesta = mng.recuperarClaveSMS(user);
            apiResp = new ApiResponse();
            apiResp.Message = "SMS enviado";
            apiResp.Data = user;
            return Ok(apiResp);
        }

    }
}