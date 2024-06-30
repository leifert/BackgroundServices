using MyQuartz.JobFactory;
using MyQuartz.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MyQuartz
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddSingleton<IJobFactory, MyJobFactory>();
            builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            builder.Services.AddSingleton<NotificationJob>();

            builder.Services.AddSingleton(new JobModel(Guid.NewGuid(), typeof(NotificationJob), "Notify Job", "0 0 12 * * ?"));
            builder.Services.AddHostedService<MyScheduler>();

            var host = builder.Build();
            host.Run();
        }
    }
}