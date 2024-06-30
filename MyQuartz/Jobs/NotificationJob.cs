using Quartz;

namespace MyQuartz.Jobs
{
    public class NotificationJob : IJob
    {
        private readonly ILogger _logger;
        public NotificationJob(ILogger<NotificationJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Notify at {DateTime.Now} JobType: {context.JobDetail.JobType}");
            return Task.CompletedTask;
        }
    }
}
