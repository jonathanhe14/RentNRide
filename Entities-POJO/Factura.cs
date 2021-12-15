using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Factura: BaseEntity
    {
        public int IdFactura { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Identificacion { get; set; }
        public string Consecutivo { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public decimal Monto { get; set; }

    }
}
