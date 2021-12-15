using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class ConsultaReserva : BaseEntity
    {
        public int Id_Vehiculo { get; set; }
        public DateTime Fecha_Reserva { get; set; }
        public string Hora_Inicio { get; set; }
        public string Hora_Fin { get; set; }
        public string Estado { get; set; }

    }
}
