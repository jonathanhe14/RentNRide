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
                    + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult GetSolicitudes() {
            try {
                apiResp = new ApiResponse();
                var mng = new UsuariosManagement();
                apiResp.Data = mng.RetrieveAllSolicitudes();

                return Ok(apiResp);
            } catch(BussinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Message));
            }
        }



        public IHttpActionResult Get(string correo)
        {
            try
            {
                var mng = new UsuariosManagement();
                var usuario = new Usuarios
                {
                    Correo = correo
                };

                usuario = mng.RetrieveById(usuario);
                apiResp = new ApiResponse();
                apiResp.Data = usuario;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // POST 
        // CREATE
        public IHttpActionResult Post(Usuarios usuario)
        {

            try
            {
                int edad = DateTime.Now.Year - usuario.FechaNacimiento.Year;

                usuario.Edad = edad;
                var mng = new UsuariosManagement();
                mng.Create(usuario);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Message));
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
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
  
        public IHttpActionResult GetRoles() {
            apiResp = new ApiResponse();
            var mng = new RolesUManager();
            apiResp.Data = mng.RetrieveAll();
            apiResp.Message = "Roles obtenidos";

            return Ok(apiResp);
        }


    }
}