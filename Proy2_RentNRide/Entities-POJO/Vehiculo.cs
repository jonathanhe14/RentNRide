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
        public double Kilometraje { get; set; }
        public double cKmExcedido { get; set; }
        public double cMalEstado { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public double cLugarDiferente { get; set; }
        public double Tarifa { get; set; }
        public string AccptInmediata { get; set; }
        public string Estado { get; set; }
        public string Imagen { get; set; }
        public int idUsuario { get; set; }


    }
}
