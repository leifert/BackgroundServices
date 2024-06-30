using Coravel;
using MyCoravel.Invocables;
using MyCoravel.Services;
namespace MyCoravel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            builder.Services.AddWindowsService();

            builder.Services.AddScheduler();
            builder.Services.AddTransient<EmailService>();
            builder.Services.AddTransient<EverySecondsInvocableJob>();


            var host = builder.Build();

            host.Services.UseScheduler(scheduler =>
            {
                var jobSchedule = scheduler.Schedule<EmailService>();
                jobSchedule.EverySeconds(3);
            });
            host.Run();
        }
    }
}