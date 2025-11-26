using Aplicacion.DTOs;
using Aplicacion.Helper.Servicios;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Caracteristicas.LicenciaSoftware
{
    public class ObtenerTodasLasLicencias
    {
        public class Consulta():IRequest<IReadOnlyCollection<LicenciaSoftwareDTO>>;
        public class Handler : IRequestHandler<Consulta, IReadOnlyCollection<LicenciaSoftwareDTO>>
        {
            private readonly ContextoDB contextoDB;
            private readonly IMapper mapper;
            private readonly IServicioUsuarioActual usuarioActual;

            public Handler(ContextoDB contextoDB, IMapper mapper, IServicioUsuarioActual usuarioActual)
            {
                this.contextoDB = contextoDB;
                this.mapper = mapper;
                this.usuarioActual = usuarioActual;
            }
            public async Task<IReadOnlyCollection<LicenciaSoftwareDTO>> Handle(Consulta request, CancellationToken cancellationToken)
            {
                var licencias = await contextoDB.LicenciaSoftware
                    .AsNoTracking()
                    .Where(x => x.IdUsuario == int.Parse(usuarioActual.Id) && !x.Eliminado)
                    .OrderBy(x => x.FechaRenovacion)
                    .ToListAsync();
                return mapper.Map<IReadOnlyCollection<LicenciaSoftwareDTO>>(licencias);
            }
        }
    }
}
