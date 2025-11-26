using Aplicacion.Dominio.Entidades.LicenciaSoftware.Enums;
using Aplicacion.Dominio.Entidades.LicenciaSoftware.Errores;
using Aplicacion.DTOs;
using Aplicacion.Helper.Servicios;
using Aplicacion.Infraestructura.Persistencia;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Throw;

namespace Aplicacion.Caracteristicas.LicenciaSoftware
{
    public class EditarLicenciaSoftware
    {
        public record DatosEditarLicenciaSoftware(int? Id, string? Nombre, TiposDeLicencia? Tipo, string? Proveedor, DateTimeOffset? FechaCompra, double? Precio,
           DateTimeOffset? FechaRenovacion, bool? IncluyeSoporte, string? EmailSoporte
       );
        public record Comando(DatosEditarLicenciaSoftware Datos) : IRequest<LicenciaSoftwareDTO>;
        public class ValidarComando : AbstractValidator<Comando>
        {
            public ValidarComando()
            {
                RuleFor(x => x.Datos.Id).NotNull();
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
                var licencia = await contextoDB.LicenciaSoftware.FirstOrDefaultAsync(x => x.Id == request.Datos.Id!.Value && !x.Eliminado);
                licencia.ThrowIfNull(() => new ErroresLicenciaSoftware.NoExisteLaLicencia());

                licencia.Editar(request.Datos.Nombre!, request.Datos.Tipo!.Value, request.Datos.Proveedor!, request.Datos.FechaCompra!.Value, 
                        request.Datos.Precio!.Value, request.Datos.FechaRenovacion!.Value, request.Datos.IncluyeSoporte!.Value, request.Datos.EmailSoporte);
                await contextoDB.SaveChangesAsync();
                return mapper.Map<LicenciaSoftwareDTO>(licencia);
            }
        }
    }
}
