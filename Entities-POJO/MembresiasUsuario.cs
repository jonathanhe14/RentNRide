using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
   public class MembresiasUsuario : BaseEntity
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public int IdMembresia { get; set; }

        public string Estado { get; set; }

        public string Comprobante { get; set; }


        public MembresiasUsuario()
        {

        }
    }
}
