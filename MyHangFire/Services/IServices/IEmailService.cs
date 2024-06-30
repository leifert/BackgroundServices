namespace MyHangFire.Services.IServices
{
    public interface IEmailService
    {
        void SendWelcomeEmail(string email, string name);
        void SendGettingStartedEmail(string email, string name);
    }
}
