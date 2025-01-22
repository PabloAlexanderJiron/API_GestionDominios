using Aplicacion.Infraestructura.EnviarEmail.Interfaces;
using Aplicacion.Infraestructura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Caracteristicas.DominioInternet
{
    public class EnviarRecordatorioDeActualizacion
    {
        public record Consulta: IRequest<int>;
        public class Handler : IRequestHandler<Consulta, int>
        {
            private readonly ContextoDB contexto;
            private readonly IEnviarEmail enviarEmail;

            public Handler(ContextoDB contexto, IEnviarEmail enviarEmail)
            {
                this.contexto = contexto;
                this.enviarEmail = enviarEmail;
            }
            public async Task<int> Handle(Consulta request, CancellationToken cancellationToken)
            {
                var dominiosPorCaducar = await contexto.DominioInternet
                    .AsNoTracking()
                    .Where(x => (x.FechaRenovacion.Date - DateTimeOffset.Now.Date).Days <= 30 && (x.FechaRenovacion.Date - DateTimeOffset.Now.Date).Days > 0)
                    .GroupBy(x => x.UsuarioCreacionLog)
                    .ToListAsync(cancellationToken);
                foreach (var usuario in dominiosPorCaducar)
                {
                    var body = $"""
                        <h3>Tiene {usuario.Count()} dominio/os por caducar</h3>
                    """;
                    foreach (var dominio in usuario)
                    {
                        var diasParaCaducar = (dominio.FechaRenovacion.Date - DateTimeOffset.Now.Date).Days;
                        body += $"<p>El dominio '{dominio.Direccion}' caducará en {diasParaCaducar} días</p>";
                    }
                    await this.enviarEmail.Ejecutar(usuario.Key, "Renovar Dominio/os",body);
                    await Task.Delay(3000);//Para no se detectado como SPAM
                }
                return dominiosPorCaducar.Count;
            }
        }
    }
}
