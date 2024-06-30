using Coravel.Scheduling.Schedule.Interfaces;
using MyCoravel.Invocables;

namespace MyCoravel
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IScheduler _scheduler;

        public Worker(ILogger<Worker> logger, IScheduler scheduler)
        {
            _logger = logger;
            _scheduler = scheduler;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker is starting...");
            try
            {

                _logger.LogInformation($"Adding job EverySecondsInvocableJob.");

                _scheduler.Schedule<EverySecondsInvocableJob>()
                            .EverySeconds(1);

                _logger.LogInformation("Worker is started.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"error: {ex}");
            }

            await Task.CompletedTask;
        }
    }
}
