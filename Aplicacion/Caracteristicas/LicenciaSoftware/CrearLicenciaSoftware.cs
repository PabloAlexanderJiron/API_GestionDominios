using Aplicacion.Dominio.Entidades.DominioInternet.Errores;
using Aplicacion.Dominio.Entidades.LicenciaSoftware;
using Aplicacion.Dominio.Entidades.LicenciaSoftware.Enums;
using Aplicacion.DTOs;
using Aplicacion.Helper.Servicios;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using LicenciaSoftwareDominio = Aplicacion.Dominio.Entidades.LicenciaSoftware.LicenciaSoftware;

namespace Aplicacion.Caracteristicas.LicenciaSoftware
{
    public class CrearLicenciaSoftware
    {
        public record DatosCrearLicenciaSoftware(string? Nombre, TiposDeLicencia? Tipo, string? Proveedor, DateTimeOffset? FechaCompra, double? Precio,
            DateTimeOffset? FechaRenovacion, bool? IncluyeSoporte, string? EmailSoporte
        );
        public record Comando(DatosCrearLicenciaSoftware Datos) : IRequest<LicenciaSoftwareDTO>;
        public class ValidarComando : AbstractValidator<Comando>
        {
            public ValidarComando()
            {
                RuleFor(x => x.Datos.Nombre).NotNull().NotEmpty().MaximumLength(100);
                RuleFor(x => x.Datos.Tipo).NotNull();
                RuleFor(x => x.Datos.Proveedor).NotNull().NotEmpty().MaximumLength(100);
                RuleFor(x => x.Datos.FechaCompra).NotNull().NotEmpty();
                RuleFor(x => x.Datos.FechaRenovacion).NotNull().NotEmpty();
                RuleFor(x => x.Datos.Precio).NotNull().GreaterThan(-1);
                RuleFor(x => x.Datos.IncluyeSoporte).NotNull();
            }
        }

        public class Handler : IRequestHandler<Comando, LicenciaSoftwareDTO>
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
            public async Task<LicenciaSoftwareDTO> Handle(Comando request, CancellationToken cancellationToken)
            {
                var licencia = LicenciaSoftwareDominio.Crear(
                    request.Datos.Nombre!,
                    request.Datos.Tipo!.Value,
                    request.Datos.Proveedor!, 
                    request.Datos.FechaCompra!.Value,
                    request.Datos.Precio!.Value, 
                    request.Datos.FechaRenovacion!.Value, 
                    request.Datos.IncluyeSoporte!.Value,
                    request.Datos.EmailSoporte!,
                    int.Parse(usuarioActual.Id));

                contextoDB.LicenciaSoftware.Add(licencia);
                await contextoDB.SaveChangesAsync(cancellationToken);
                return mapper.Map<LicenciaSoftwareDTO>(licencia);
            }
            public class MapRespuesta : Profile
            {
                public MapRespuesta()
                {
                    CreateMap<LicenciaSoftwareDominio, LicenciaSoftwareDTO>();
                }
            }
        }
    }
}
