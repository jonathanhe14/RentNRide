using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Horas : BaseEntity
    {
        public int Id_Vehiculo { get; set; }
        public int Id_Horario { get; set; }
        public int Id_Hora { get; set; }
        public int Dia { get; set; }
        public string Hora_Inicio { get; set; }
        public string Hora_Final { get; set; }
        public string Disponibilidad { get; set; }
        public string Estado { get; set; }

    }
}
