using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Usuarios : BaseEntity
    {
        public int Id_usuario { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string PersoneriaJuridica { get; set; }
        public string PermisoOperaciones { get; set; }
        public string Estado { get; set; }
        public int OTP { get; set; }
        public int Rol { get; set; }
        
        public string Comprobacion { get; set; }

        public string ContrassenaActual { get; set; }

        public int OTPSMS { get; set; }
        public Usuarios()
        {

        }

    }
}
