﻿using MyHangFire.Services.IServices;

namespace MyHangFire.Services
{
    public class EmailService : IEmailService
    {
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
