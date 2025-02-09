namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string email, string subject, string message);
    }
}
