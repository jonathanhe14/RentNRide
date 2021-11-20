using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
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



        public IHttpActionResult Get(string correo,string cedula)
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
                int edad =0;
                if (usuario.FechaNacimiento.Year<DateTime.Now.Year)
                {
                    edad = DateTime.Now.Year - usuario.FechaNacimiento.Year;
                    if (usuario.FechaNacimiento.Month > DateTime.Now.Month)
                    {
                        edad--;

                    }else if (usuario.FechaNacimiento.Month == DateTime.Now.Month)
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



    }
}