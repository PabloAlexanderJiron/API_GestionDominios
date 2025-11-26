using Aplicacion.Dominio.Comunes;
using Aplicacion.Dominio.Entidades.LicenciaSoftware.Enums;
using Aplicacion.Dominio.Entidades.LicenciaSoftware.Errores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Throw;

namespace Aplicacion.Dominio.Entidades.LicenciaSoftware
{
    public class LicenciaSoftware: EntidadAuditable
    {
        public string Nombre { get; set; } = string.Empty;
        public TiposDeLicencia Tipo { get; set; }
        public string Proveedor { get; set; } = string.Empty;
        public DateTimeOffset FechaCompra { get; set; }
        public double Precio { get; set; }
        public DateTimeOffset FechaRenovacion { get; set; }
        public bool IncluyeSoporte { get; set; }
        public string? EmailSoporte { get; set; }
        public int IdUsuario { get; set; }

        public static LicenciaSoftware Crear(string nombre, TiposDeLicencia tipo, string proveedor, DateTimeOffset fechaCompra, 
            double precio, DateTimeOffset fechaRenovacion, bool incluyeSoporte, string? emailSoporte, int idUsuario)
        {
            return new LicenciaSoftware() { 
                Nombre = nombre.Trim(),
                Tipo = tipo,
                Proveedor = proveedor.Trim(),
                FechaCompra = fechaCompra,
                Precio = precio,
                FechaRenovacion = fechaRenovacion,
                EmailSoporte = emailSoporte,
                IncluyeSoporte = incluyeSoporte,
                IdUsuario = idUsuario
            };
        }

        public void Editar(string nombre, TiposDeLicencia tipo, string proveedor, DateTimeOffset fechaCompra,
            double precio, DateTimeOffset fechaRenovacion, bool incluyeSoporte, string? emailSoporte)
        {
            fechaCompra.Throw(() => new ErroresLicenciaSoftware.FechaRenovacionMayorQueLaCompra())
                .IfTrue(x => x >= fechaRenovacion);
            Nombre = nombre.Trim();
            Tipo = tipo;
            Proveedor = proveedor;
            FechaCompra = fechaCompra;
            Precio = precio;
            FechaRenovacion = fechaRenovacion;
            EmailSoporte = emailSoporte;
            IncluyeSoporte = incluyeSoporte;
        }

        public void Eliminar()
        {
            Eliminado = true;
        }
    }
}
