using Coravel.Invocable;

namespace MyCoravel.Invocables
{
    public class EverySecondsInvocableJob : IInvocable
    {
        private readonly ILogger<EverySecondsInvocableJob> _logger;
        public EverySecondsInvocableJob(ILogger<EverySecondsInvocableJob> logger)
        {
            _logger = logger;
        }
        public Task Invoke()
        {
            _logger.LogInformation($"Job {nameof(EverySecondsInvocableJob)} was invoked.");
            return Task.CompletedTask;
        }
    }
}
