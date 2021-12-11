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
    public class ReservaController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        [HttpPost]
        public IHttpActionResult CrearReserva(Reserva reserva)
        {
            try
            {
                var mng = new ReservaManager();
                mng.Create(reserva);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {

                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        [HttpGet]
        public IHttpActionResult RecuperarTodo()
        {

            try
            {
                apiResp = new ApiResponse();
                var mng = new ReservaManager();
                apiResp.Data = mng.RetrieveAll();

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Mensaje));
            }

        }

        [HttpPut]
        public IHttpActionResult ActualizarReserva(Reserva reserva)
        {
            try
            {
                var mng = new ReservaManager();
                mng.Update(reserva);

                apiResp = new ApiResponse();
                apiResp.Message = "Action was executed.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        [HttpGet]
        public IHttpActionResult ReservasPorUsuario(string correo)
        {
            try
            {
                var mng = new ReservaManager();
                Reserva reserva = new Reserva();
                reserva.Usuario = correo;
                List<Reserva> reservas = mng.RetrieveAllById(reserva);
                apiResp = new ApiResponse();
                apiResp.Data = reservas;
                apiResp.Message = "Action was executed.";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }
        }

        [HttpDelete]
        public IHttpActionResult BorrarReserva(Reserva reserva)
        {
            try
            {
                var mng = new ReservaManager();
                //Reserva reserva = new Reserva();
                //reserva.Id_Reserva = id_reserva;
                mng.Delete(reserva);

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