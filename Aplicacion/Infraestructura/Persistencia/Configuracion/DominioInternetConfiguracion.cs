using Aplicacion.Dominio.Entidades.DominioInternet;
using Aplicacion.Infraestructura.Persistencia.Comunes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Infraestructura.Persistencia.Configuracion
{
    public class DominioInternetConfiguracion : IEntityTypeConfiguration<DominioInternet>
    {
        public void Configure(EntityTypeBuilder<DominioInternet> entity)
        {
            AuditableConfiguracion.Configurar(entity);

            entity.Property(a => a.Direccion)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.Proveedor)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.FechaCompra)
                .IsRequired();

            entity.Property(x => x.Precio)
                .IsRequired();

            entity.Property(x => x.FechaRenovacion)
                .IsRequired();
        }
    }
}
