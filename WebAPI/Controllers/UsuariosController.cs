using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using WebAPI.Models;
using CoreAPI;
using Entities_POJO;
using Exceptions;


namespace WebAPI.Controllers
{
    public class UsuariosController : ApiController
    {
        // GET: Usuarios
        ApiResponse apiResp = new ApiResponse();


        public IHttpActionResult Get()
        {
            try
            {
                apiResp = new ApiResponse();
                var mng = new UsuariosManagement();
                apiResp.Data = mng.RetrieveAll();

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Mensaje));
            }
        }



        public IHttpActionResult Get(string correo, string cedula)
        {
            try
            {
                var mng = new UsuariosManagement();
                var usuario = new Usuarios
                {
                    Correo = correo,
                    Cedula = cedula
                };

                usuario = mng.TraerUsuario(usuario);
                apiResp = new ApiResponse();
                apiResp.Data = usuario;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        // POST 
        // CREATE
        public IHttpActionResult CrearUsuario(Usuarios usuario)
        {

            try
            {
                int edad = 0;
                if (usuario.FechaNacimiento.Year < DateTime.Now.Year)
                {
                    edad = DateTime.Now.Year - usuario.FechaNacimiento.Year;
                    if (usuario.FechaNacimiento.Month > DateTime.Now.Month)
                    {
                        edad--;

                    }
                    else if (usuario.FechaNacimiento.Month == DateTime.Now.Month)
                    {
                        if (usuario.FechaNacimiento.Day > DateTime.Now.Day)
                        {
                            edad--;
                        }
                    }
                }

                usuario.Edad = edad;
                var mng = new UsuariosManagement();
                mng.CreateUsuario(usuario);
                mng.EvioOTPCorreo(usuario);
                mng.EnvioOTPSMS(usuario);
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

        public IHttpActionResult CrearSocio(Usuarios usuario)
        {

            try
            {

                var mng = new UsuariosManagement();
                mng.CreateSocio(usuario);
                mng.EvioOTPCorreo(usuario);
                mng.EnvioOTPSMS(usuario);
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

        public IHttpActionResult ReenviarCodigos(Usuarios usuario)
        {
            try
            {
                var mng = new UsuariosManagement();
                mng.EvioOTPCorreo(usuario);
                mng.EnvioOTPSMS(usuario);
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
        public IHttpActionResult Put(Usuarios usuarios)
        {
            try
            {
                var mng = new UsuariosManagement();
                mng.Update(usuarios);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        [HttpPost]
        public IHttpActionResult EnvioOTP(Usuarios user)
        {
            var mng = new UsuariosManagement();
            Usuarios respuesta = mng.EvioOTPCorreo(user);
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

        [HttpPost]
        public IHttpActionResult EnvioOTPSMS(Usuarios user)
        {
            var mng = new UsuariosManagement();
            Usuarios respuesta = mng.EnvioOTPSMS(user);
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


        [HttpPost]
        public IHttpActionResult ComprobarOTP(Usuarios user)
        {
            try
            {
                var mng = new UsuariosManagement();
                string respuesta = mng.validarOTP(user);
                apiResp = new ApiResponse();
                apiResp.Message = respuesta;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Mensaje));
            }
        }

        [HttpGet]
        public IHttpActionResult GetRoles()
        {
            apiResp = new ApiResponse();
            var mng = new RolesUManager();
            apiResp.Data = mng.RetrieveAll();
            apiResp.Message = "Roles obtenidos";

            return Ok(apiResp);
        }

    }
}