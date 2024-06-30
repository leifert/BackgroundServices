using Quartz;
using Quartz.Spi;

namespace MyQuartz
{
    public class MyScheduler : IHostedService
    {
        public IScheduler Scheduler { get; set; }
        private readonly IJobFactory _jobFactory;
        private readonly JobModel _jobModel;
        private readonly ISchedulerFactory _schedulerFactory;

        public MyScheduler(ISchedulerFactory schedulerFactory, JobModel jobModel, IJobFactory jobFactory)
        {
            _schedulerFactory = schedulerFactory;
            _jobModel = jobModel;
            _jobFactory = jobFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler();
            Scheduler.JobFactory = _jobFactory;

            IJobDetail jobDetail = CreateJob(_jobModel);

            ITrigger trigger = CreateTrigger(_jobModel);

            await Scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);

            await Scheduler.Start(cancellationToken);
        }

        private IJobDetail CreateJob(JobModel jobModel)
        {
            return JobBuilder.Create(jobModel.JobType)
                            .WithIdentity(jobModel.JobId.ToString())
                            .WithDescription(jobModel.JobName)
                            .Build();
        }

        private ITrigger CreateTrigger(JobModel jobModel)
        {
            return TriggerBuilder.Create()
                                .WithIdentity(jobModel.JobId.ToString())
                                .WithCronSchedule(jobModel.CronExpression)
                                .WithDescription(jobModel.JobName)
                                .Build();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
