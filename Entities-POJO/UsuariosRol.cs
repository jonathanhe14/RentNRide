using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class UsuariosRol : BaseEntity
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; }
        public string Estado { get; set; }
        public int IdRol { get; set; }

        public UsuariosRol()
        {

        }
    }
}