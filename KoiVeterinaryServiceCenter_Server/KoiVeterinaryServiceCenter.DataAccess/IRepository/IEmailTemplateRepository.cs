using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface IEmailTemplateRepository : IRepository<EmailTemplate>
{
    void Update(EmailTemplate emailTemplate);
}