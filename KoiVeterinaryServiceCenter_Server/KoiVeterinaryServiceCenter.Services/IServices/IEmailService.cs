namespace KoiVeterinaryServiceCenter.Services.IServices;

public interface IEmailService
{
    Task<bool> SendEmailToClientAsync(string toEmail, string subject, string body);
    Task<bool> SendVerifyEmail(string toMail, string confirmationLink);
}