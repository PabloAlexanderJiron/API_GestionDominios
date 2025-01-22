using Aplicacion.DTOs;
using Aplicacion.Helper.Servicios;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using InternetDominio = Aplicacion.Dominio.Entidades.DominioInternet.DominioInternet;

namespace Aplicacion.Caracteristicas.DominioInternet
{
    public class ObtenerDominiosInternet
    {
        public record Consulta():IRequest<IReadOnlyCollection<DominioInternetDTO>>;
        public class Handler : IRequestHandler<Consulta, IReadOnlyCollection<DominioInternetDTO>>
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
            public async Task<IReadOnlyCollection<DominioInternetDTO>> Handle(Consulta request, CancellationToken cancellationToken)
            {
                var dominios = await contextoDB.DominioInternet
                    .AsNoTracking()
                    .Where(x => x.IdUsuario == int.Parse(usuarioActual.Id) && !x.Eliminado)
                    .ToListAsync();
                return mapper.Map<IReadOnlyCollection<DominioInternetDTO>>(dominios);
            }

            public class MapRespuesta : Profile
            {
                public MapRespuesta()
                {
                    CreateMap<InternetDominio, DominioInternetDTO>();
                }
            }
        }
    }
}
