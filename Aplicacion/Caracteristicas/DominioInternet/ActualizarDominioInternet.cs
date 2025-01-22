using Aplicacion.Dominio.Entidades.DominioInternet.Errores;
using Aplicacion.DTOs;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Throw;
using InternetDominio = Aplicacion.Dominio.Entidades.DominioInternet.DominioInternet;

namespace Aplicacion.Caracteristicas.DominioInternet
{
    public class ActualizarDominioInternet
    {
        public record DatosActualizarDominioInternet(int? Id, string? Proveedor, DateTimeOffset? FechaCompra, double? Precio, DateTimeOffset? FechaRenovacion);
        public record Comando(DatosActualizarDominioInternet Datos) : IRequest<DominioInternetDTO>;
        public class ValidarComando : AbstractValidator<Comando>
        {
            public ValidarComando()
            {
                RuleFor(x => x.Datos.Id).NotNull();
                RuleFor(x => x.Datos.Proveedor).NotNull().NotEmpty().MaximumLength(100);
                RuleFor(x => x.Datos.FechaCompra).NotNull().NotEmpty();
                RuleFor(x => x.Datos.FechaRenovacion).NotNull().NotEmpty();
                RuleFor(x => x.Datos.Precio).NotNull().NotEmpty();
            }
        }
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
                var dominio = await contextoDB.DominioInternet.FirstOrDefaultAsync(x => x.Id == request.Datos.Id!.Value && !x.Eliminado);
                dominio.ThrowIfNull(() => new ErroresDominioInternet.NoExisteElDominio());
                
                dominio.Actualizar(request.Datos.Proveedor!, request.Datos.FechaCompra!.Value, request.Datos.Precio!.Value, request.Datos.FechaRenovacion!.Value);
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
