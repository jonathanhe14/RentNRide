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
        public IHttpActionResult CrearReserva(List<Reserva> listaReservas)
        {
            try
            {
                var mng = new ReservaManager();
                int horas = listaReservas.Count;
                apiResp = new ApiResponse();
                foreach (var reserva in listaReservas)
                {
                    mng.Create(reserva, horas);
                }
                if (!listaReservas[0].Solicitud.Equals("PENDIENTE"))
                {
                    apiResp.Message = "Su reserva ha sido generada";
                    mng.CrearFactura(listaReservas);
                    return Ok(apiResp);
                }
                apiResp.Message = "Solicitud creada, por favor esperar confirmación. " +
                    "Una vez que se haya aprobado la solicitud, se efectuará el cargo en su monedero";

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

        [HttpPost]
        public IHttpActionResult ConsultasReserva(List<ConsultaReserva> listaConsultas)
        {
            //try
            //{
            var mng = new ReservaManager();
            List<ConsultaReserva> consultas = mng.RetrieveDisponibility(listaConsultas);
            apiResp = new ApiResponse();
            apiResp.Data = consultas;
            apiResp.Message = "Action was excecuted.";
            return Ok(apiResp);
            /*}
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Mensaje));
            }*/
        }


    }
}