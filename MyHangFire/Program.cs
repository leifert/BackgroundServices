
using Hangfire;
using Hangfire.Storage.SQLite;
using HangfireBasicAuthenticationFilter;
using MyHangFire.Services;
using MyHangFire.Services.IServices;

namespace MyHangFire
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IEmailService, EmailService>();

            //Hangfire client
            builder.Services.AddHangfire(config => config
                            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                            .UseSimpleAssemblyNameTypeSerializer()
                            .UseRecommendedSerializerSettings()
                            .UseSQLiteStorage(hangfireConnectionString));

            //Hangfire server
            builder.Services.AddHangfireServer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHangfireDashboard();
            app.MapHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = "My Dash",
                Authorization = new[]
                {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        Pass = "pass",
                        User = "admin"
                    }
                }
            });


            //Recurring job
            RecurringJob.AddOrUpdate("Hello", () => Console.WriteLine("Hello from hangfire"), Cron.Minutely);

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
