using Aplicacion.Dominio.Entidades.LicenciaSoftware;
using Aplicacion.Dominio.Entidades.LicenciaSoftware.Enums;
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
    public class LicenciaSoftwareConfiguracion : IEntityTypeConfiguration<LicenciaSoftware>
    {
        public void Configure(EntityTypeBuilder<LicenciaSoftware> entity)
        {
            AuditableConfiguracion.Configurar(entity);

            entity.Property(x => x.Tipo)
                .HasConversion(
                    x => (int)x,
                    x => (TiposDeLicencia)x
                )
                .IsRequired();
        }
    }
}
