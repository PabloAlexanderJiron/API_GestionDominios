
using Aplicacion.Caracteristicas.DominioInternet;
using MediatR;

namespace API_GestionDominios.CronJobs
{
    public class EnviarEmailsDominiosPorCaducarCronJob : IHostedService, IDisposable
    {
        private Timer? timer = null;
        private readonly ILogger<EnviarEmailsDominiosPorCaducarCronJob> logger;
        private readonly IServiceScopeFactory scopeFactory;

        public EnviarEmailsDominiosPorCaducarCronJob(ILogger<EnviarEmailsDominiosPorCaducarCronJob> logger, IServiceScopeFactory scopeFactory)
        {
            this.logger = logger;
            this.scopeFactory = scopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            EstableceTiempoEjecucion();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        private void EstableceTiempoEjecucion()
        {
            // Obtener la hora actual en Ecuador (UTC-5)
            DateTime fechaActual = DateTime.UtcNow.AddHours(-5);

            // Definir las 10:30 AM en Ecuador para hoy
            var hora = int.Parse(Environment.GetEnvironmentVariable("HORA_ENVIO_EMAIL") ?? "7");
            var minuto = int.Parse(Environment.GetEnvironmentVariable("MINUTO_ENVIO_EMAIL") ?? "0");
            DateTime proximaEjecucion = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, hora, minuto, 0);

            if(fechaActual >= proximaEjecucion)
                proximaEjecucion = proximaEjecucion.AddDays(1);

            var tiempoEspera = proximaEjecucion - fechaActual;
            timer = new Timer(EnviarEmails, null, tiempoEspera, Timeout.InfiniteTimeSpan);
        }

        private async void EnviarEmails(object? o)
        {
            try
            {
                using var scope = scopeFactory.CreateScope();

                var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
                var emailsEnviados = await mediator.Send(new EnviarRecordatorioDeActualizacion.Consulta());
                logger.LogInformation($"Se enviaron {emailsEnviados} emails");
            }
            catch (Exception e)
            {
                logger.LogError($"Error al enviar los correos automaticos({e.Message}): {e.StackTrace}");
            }
            finally
            {
                EstableceTiempoEjecucion();
            }
        }
    }
}
