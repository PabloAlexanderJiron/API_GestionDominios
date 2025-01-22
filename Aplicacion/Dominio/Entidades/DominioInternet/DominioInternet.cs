using Aplicacion.Dominio.Comunes;
using Aplicacion.Dominio.Entidades.DominioInternet.Errores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Throw;

namespace Aplicacion.Dominio.Entidades.DominioInternet
{
    public class DominioInternet:EntidadAuditable
    {
        public string Direccion { get; set; } = string.Empty;
        public string Proveedor { get; set; } = string.Empty;
        public DateTimeOffset FechaCompra { get; set; }
        public double Precio { get; set; }
        public DateTimeOffset FechaRenovacion { get; set; }
        public int IdUsuario { get; set; }

        public static DominioInternet Crear(string direccion, string proveedor, DateTimeOffset fechaCompra, double precio, DateTimeOffset fechaRenovacion, int idUsuario) {
            fechaCompra.Throw(() => new ErroresDominioInternet.FechaRenovacionMayorQueLaCompra())
                .IfTrue(x => x >= fechaRenovacion);
            return new DominioInternet()
            {
                Direccion = direccion.Trim().ToLower(),
                Proveedor = proveedor.Trim(),
                FechaCompra = fechaCompra,
                Precio = precio,
                FechaRenovacion = fechaRenovacion,
                IdUsuario = idUsuario
            };
        }

        public void Actualizar(string proveedor, DateTimeOffset fechaCompra, double precio, DateTimeOffset fechaRenovacion)
        {
            fechaCompra.Throw(() => new ErroresDominioInternet.FechaRenovacionMayorQueLaCompra())
                .IfTrue(x => x >= fechaRenovacion);
            Proveedor = proveedor.Trim();
            FechaCompra = fechaCompra;
            Precio = precio;
            FechaRenovacion = fechaRenovacion;
        }

        public void Eliminar()
        {
            Eliminado = true;
        }
    }
}
