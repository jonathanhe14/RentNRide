using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_POJO
{
    public class Vehiculo : BaseEntity
    {

        public int Id { get; set; }
        public int Tipo { get; set; }
        public int Combustible { get; set; }
        public int Modelo { get; set; }
        public int Marca { get; set; }
        public decimal Kilometraje { get; set; }
        public decimal cKmExcedido { get; set; }
        public decimal cMalEstado { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public decimal cLugarDiferente { get; set; }
        public decimal Tarifa { get; set; }
        public string AccptInmediata { get; set; }
        public string Estado { get; set; }
        public string Imagen { get; set; }
        public string idUsuario { get; set; }


    }
}
