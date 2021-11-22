using CoreAPI;
using Entities_POJO;
using Exceptions;
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
            try
            {
                var mng = new UserProfileManager();
                user = mng.ValidateUser(user);
                apiResp = new ApiResponse();
                apiResp.Data = user;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        [HttpPost]
        public IHttpActionResult RecuperarClaveCorreo(Usuarios user)
        {
            try
            {
                var mng = new UserProfileManager();
                Usuarios respuesta = mng.recuperarClaveCorreo(user);
                apiResp = new ApiResponse();
                apiResp.Data = respuesta;
                if (respuesta != null)
                {
                    apiResp.Message = "success";
                }
                else
                {
                    apiResp.Message = "El usuario no fue encontrado";
                }
                return Ok(apiResp);

            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        [HttpPost]
        public IHttpActionResult RecuperarClaveSMS(Usuarios user)
        {
            try
            {
                var mng = new UserProfileManager();
                Usuarios respuesta = mng.recuperarClaveTelefono(user);
                apiResp = new ApiResponse();
                apiResp.Data = respuesta;
                if (respuesta != null)
                {
                    apiResp.Message = "success";
                }
                else
                {
                    apiResp.Message = "El usuario no fue encontrado";
                }
                return Ok(apiResp);

            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        [HttpPost]
        public IHttpActionResult ComprobarOTP(Usuarios user)
        {
            try
            {
                var mng = new UserProfileManager();
                string respuesta = mng.validarOTP(user);
                apiResp = new ApiResponse();
                apiResp.Message = respuesta;
                apiResp.Data = user;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }

        }

        [HttpPost]
        public IHttpActionResult CambiarClave(Usuarios user)
        {
            try
            {
                var mng = new UserProfileManager();
                string respuesta = mng.actualizarClave(user);
                apiResp = new ApiResponse();
                apiResp.Message = respuesta;
                apiResp.Data = user;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }


    }
}