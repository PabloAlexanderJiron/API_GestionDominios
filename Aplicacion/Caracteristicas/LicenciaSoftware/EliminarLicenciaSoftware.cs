using Aplicacion.Dominio.Entidades.DominioInternet.Errores;
using Aplicacion.DTOs;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Throw;

namespace Aplicacion.Caracteristicas.LicenciaSoftware
{
    public class EliminarLicenciaSoftware
    {
        public record Comando(int Id) : IRequest<LicenciaSoftwareDTO>;
        public class Handler : IRequestHandler<Comando, LicenciaSoftwareDTO>
        {
            private readonly ContextoDB contextoDB;
            private readonly IMapper mapper;

            public Handler(ContextoDB contextoDB, IMapper mapper)
            {
                this.contextoDB = contextoDB;
                this.mapper = mapper;
            }
            public async Task<LicenciaSoftwareDTO> Handle(Comando request, CancellationToken cancellationToken)
            {
                var dominio = await contextoDB.LicenciaSoftware.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Eliminado);
                dominio.ThrowIfNull(() => new ErroresDominioInternet.NoExisteElDominio());

                dominio.Eliminar();

                await contextoDB.SaveChangesAsync();
                return mapper.Map<LicenciaSoftwareDTO>(dominio);
            }
        }
    }
}
