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

        [HttpPost]
        public IHttpActionResult CrearHorario(List<Horario> lstHorarios)
        {
            try
            {
            var mng = new HorarioManager();
            foreach (var horario in lstHorarios)
            {
                mng.Create(horario);
            }

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

        // GET api/vehiculo
        // Retrieve
        [HttpGet]
        public IHttpActionResult RecuperarTodo()
        {

            try
            {
                apiResp = new ApiResponse();
                var mng = new HorarioManager();
                apiResp.Data = mng.RetrieveAll();

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Mensaje));
            }

        }

        // GET api/
        // Retrieve by id
        [HttpGet]
        public IHttpActionResult RecuperarUnHorario(int id)
        {
            try
            {
                var mng = new HorarioManager();
                var schedule = new Horario
                {
                    Id_Vehiculo = id
                };

                List<Horario> horarios = mng.RetrieveById(schedule);
                apiResp = new ApiResponse();
                apiResp.Data = horarios;
                apiResp.Message = "success";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }


        // PUT
        // UPDATE
        [HttpPut]
        public IHttpActionResult ActualizarHorario(Horario horario)
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
        [HttpDelete]
        public IHttpActionResult BorrarHorario(Horario horario)
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