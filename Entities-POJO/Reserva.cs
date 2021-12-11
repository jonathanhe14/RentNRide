using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Reserva : BaseEntity
    {
        public int Id_Vehiculo { get; set; }
        public int Id_Reserva { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaReserva { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string Usuario { get; set; }
        public string Socio { get; set; }
        public decimal Tarifa { get; set; }
        public decimal Comision { get; set; }
        public decimal MalEstado { get; set; }
        public decimal Entrega { get; set; }
        public decimal KmExcedido { get; set; }
        public decimal KmInicial { get; set; }
        public decimal KmFinal { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Solicitud { get; set; }
        public int CalifSocio { get; set; }
        public int CalifUsuario { get; set; }
        public string CodigoQR { get; set; }


    }
}
