using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Monedero: BaseEntity
    {
        public int IdMonedero { get; set; }
        public string IdUsuario { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaCorte { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string InfoMonedero { get; set; }
        public Monedero()
        {

        }

    }
}
