using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Contrasennas : BaseEntity
    {
        public string Correo { get; set; }
        public DateTime Fecha { get; set; }
        public string Contrasenna { get; set; }

        public Contrasennas()
        {

        }


    }
}
