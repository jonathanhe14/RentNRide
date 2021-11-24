using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO {
    public class TipoCombustible : BaseEntity {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }

    }
}
