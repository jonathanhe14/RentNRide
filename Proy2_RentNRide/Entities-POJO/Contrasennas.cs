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

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Contrasennas objAsPart)) return false;
            else return Equals(objAsPart);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }


    }
}
