
using Aplicacion.Caracteristicas.DominioInternet;
using MediatR;

namespace API_GestionDominios.CronJobs
{
    public class EnviarEmailsDominiosPorCaducarCronJob : IHostedService, IDisposable
    {
        private Timer? timer = null;
        private ISender mediator;
        private readonly ILogger<EnviarEmailsDominiosPorCaducarCronJob> logger;

        public EnviarEmailsDominiosPorCaducarCronJob(IServiceProvider serviceProvider, ILogger<EnviarEmailsDominiosPorCaducarCronJob> logger)
        {
            this.mediator = serviceProvider.GetService<ISender>()!;
            this.logger = logger;
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
            var hora = int.Parse(Environment.GetEnvironmentVariable("HORA_ENVIO_EMAIL") ?? "8");
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
