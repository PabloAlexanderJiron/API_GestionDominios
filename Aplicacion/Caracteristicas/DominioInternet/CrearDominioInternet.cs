using Aplicacion.Dominio.Entidades.DominioInternet.Errores;
using Aplicacion.DTOs;
using Aplicacion.Helper.Servicios;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using InternetDominio = Aplicacion.Dominio.Entidades.DominioInternet.DominioInternet;

namespace Aplicacion.Caracteristicas.DominioInternet
{
    public class CrearDominioInternet
    {
        public record DatosCrearDominioInternet(string? Direccion, string? Proveedor, DateTimeOffset? FechaCompra, double? Precio, DateTimeOffset? FechaRenovacion);
        public record Comando(DatosCrearDominioInternet Datos):IRequest<DominioInternetDTO>;
        public class ValidarComando : AbstractValidator<Comando>
        {
            public ValidarComando()
            {
                RuleFor(x => x.Datos.Direccion).NotNull().NotEmpty().MaximumLength(200);
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
            private readonly IServicioUsuarioActual usuarioActual;

            public Handler(ContextoDB contextoDB, IMapper mapper, IServicioUsuarioActual usuarioActual)
            {
                this.contextoDB = contextoDB;
                this.mapper = mapper;
                this.usuarioActual = usuarioActual;
            }
            public async Task<DominioInternetDTO> Handle(Comando request, CancellationToken cancellationToken)
            {
                var dominio = InternetDominio.Crear(
                    request.Datos.Direccion!, request.Datos.Proveedor!, request.Datos.FechaCompra!.Value, 
                    request.Datos.Precio!.Value, request.Datos.FechaRenovacion!.Value, int.Parse(usuarioActual.Id));

                var dominioExistente = await contextoDB.DominioInternet
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Direccion == request.Datos.Direccion!.Trim().ToLower() && !x.Eliminado);
                if (dominioExistente != null)
                    throw new ErroresDominioInternet.DominioExistente();

                contextoDB.DominioInternet.Add(dominio);
                await contextoDB.SaveChangesAsync(cancellationToken);
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
