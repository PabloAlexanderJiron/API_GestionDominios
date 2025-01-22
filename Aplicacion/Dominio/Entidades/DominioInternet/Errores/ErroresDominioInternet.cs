using Aplicacion.Helper.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Dominio.Entidades.DominioInternet.Errores
{
    public class ErroresDominioInternet
    {
        public class NoExisteElDominio : ExcepcionDominio
        {
            public NoExisteElDominio() : base("No existe el dominio!")
            {
            }
        }

        public class DominioExistente : ExcepcionDominio
        {
            public DominioExistente() : base("La direccion del dominio ya esta registrada!")
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
