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
        public IHttpActionResult RecuperarClaveCorreo(Usuarios user)
        {
            var mng = new UserProfileManager();
            string respuesta = mng.recuperarClaveCorreo(user);
            apiResp = new ApiResponse();
            apiResp.Message = respuesta;
            apiResp.Data = user;
            return Ok(apiResp);
        }

        [HttpPost]
        public IHttpActionResult RecuperarClaveSMS(Usuarios user)
        {
            var mng = new UserProfileManager();
            string respuesta = mng.recuperarClaveTelefono(user);
            apiResp = new ApiResponse();
            apiResp.Message = respuesta;
            apiResp.Data = user;
            return Ok(apiResp);
        }

        [HttpPost]
        public IHttpActionResult ComprobarOTP(Usuarios user)
        {
            var mng = new UserProfileManager();
            string respuesta = mng.validarOTP(user);
            apiResp = new ApiResponse();
            apiResp.Message = respuesta;
            apiResp.Data = user;
            return Ok(apiResp);
        }

        [HttpPost]
        public IHttpActionResult CambiarClave(Usuarios user)
        {
            var mng = new UserProfileManager();
            string respuesta = mng.actualizarClave(user);
            apiResp = new ApiResponse();
            apiResp.Message = respuesta;
            apiResp.Data = user;
            return Ok(apiResp);
        }


    }
}