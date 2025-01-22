using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class DominioInternetDTO
    {
        public int Id { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string Proveedor { get; set; } = string.Empty;
        public DateTimeOffset FechaCompra { get; set; }
        public double Precio { get; set; }
        public DateTimeOffset FechaRenovacion { get; set; }
    }
}
