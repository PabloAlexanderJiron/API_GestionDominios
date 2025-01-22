using Aplicacion.Dominio.Entidades.DominioInternet.Errores;
using Aplicacion.DTOs;
using Aplicacion.Helper.Servicios;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using InternetDominio = Aplicacion.Dominio.Entidades.DominioInternet.DominioInternet;
using Throw;

namespace Aplicacion.Caracteristicas.DominioInternet
{
    public class EliminarDominioInternet
    {
        public record Comando(int Id):IRequest<DominioInternetDTO>;
        public class Handler : IRequestHandler<Comando, DominioInternetDTO>
        {
            private readonly ContextoDB contextoDB;
            private readonly IMapper mapper;

            public Handler(ContextoDB contextoDB, IMapper mapper)
            {
                this.contextoDB = contextoDB;
                this.mapper = mapper;
            }
            public async Task<DominioInternetDTO> Handle(Comando request, CancellationToken cancellationToken)
            {
                var dominio = await contextoDB.DominioInternet.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Eliminado);
                dominio.ThrowIfNull(()=> new ErroresDominioInternet.NoExisteElDominio());

                dominio.Eliminar();

                await contextoDB.SaveChangesAsync();
                return mapper.Map<DominioInternetDTO>(dominio);
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
