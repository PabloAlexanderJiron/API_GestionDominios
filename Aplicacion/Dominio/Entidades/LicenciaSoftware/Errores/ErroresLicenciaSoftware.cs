using Aplicacion.Helper.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dominio.Entidades.LicenciaSoftware.Errores
{
    public class ErroresLicenciaSoftware
    {
        public class NoExisteLaLicencia : ExcepcionDominio
        {
            public NoExisteLaLicencia() : base("No existe la licencia!")
            {
            }
        }

        public class FechaRenovacionMayorQueLaCompra : ExcepcionDominio
        {
            public FechaRenovacionMayorQueLaCompra() : base("La fecha de renovacion es mayor que la fecha de compra!")
            {
            }
        }
    }
}
