using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO {
    public class Membresias:BaseEntity {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal MontoMensual { get; set; }

        public decimal ComisionTransaccion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int NumDias { get; set; }

        public string Activo { get; set; }
        public string Correo { get; set; }

        public Membresias() {

        }

    }
}
