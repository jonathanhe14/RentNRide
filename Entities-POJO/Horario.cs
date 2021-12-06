using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Horario : BaseEntity
    {

        public int Id_Vehiculo { get; set; }
        public string horaInicio { get; set; }
        public string horaFinal { get; set; }
        public int Id { get; set; }
        public string Disponibilidad { get; set; }
        public int DiaInicial { get; set; }
        public int DiaFinal { get; set; }

    }
}
