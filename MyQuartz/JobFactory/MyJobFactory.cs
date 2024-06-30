using Quartz;
using Quartz.Spi;

namespace MyQuartz.JobFactory
{
    public class MyJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public MyJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            return _serviceProvider.GetService(jobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            throw new NotImplementedException();
        }
    }
}
