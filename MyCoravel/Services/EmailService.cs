using Coravel.Invocable;
using MyCoravel.Services.IServices;

namespace MyCoravel.Services
{
    public class EmailService : IEmailService, IInvocable
    {
        private readonly ILogger<EmailService> _logger;
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }
        public Task Invoke()
        {
            _logger.LogInformation("Starting job");
            return Task.CompletedTask;
        }

        public void SendGettingStartedEmail(string email, string name)
        {
            Console.WriteLine($"This will send a welcome email to {name} using the following email {email}");
        }

        public void SendWelcomeEmail(string email, string name)
        {
            Console.WriteLine($"This will send a getting started email to {name} using the following email {email}");

        }
    }
}
