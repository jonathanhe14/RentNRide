using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Documento : BaseEntity
    {

        public int idVehi { get; set; }
        public string Marchamo { get; set; }
        public string tituloPropiedad { get; set; }
        public string Riteve { get; set; }
        public string derechoCirculacion { get; set; }

    }
}
