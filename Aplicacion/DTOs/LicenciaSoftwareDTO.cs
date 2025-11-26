using Aplicacion.Dominio.Entidades.LicenciaSoftware.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class LicenciaSoftwareDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public TiposDeLicencia Tipo { get; set; }
        public string Proveedor { get; set; } = string.Empty;
        public DateTimeOffset FechaCompra { get; set; }
        public double Precio { get; set; }
        public DateTimeOffset FechaRenovacion { get; set; }
        public bool IncluyeSoporte { get; set; }
        public string? EmailSoporte { get; set; }
    }
}
