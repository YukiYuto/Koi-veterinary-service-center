using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository;

public class EmailTemplateRepository : Repository<EmailTemplate>, IEmailTemplateRepository
{
    private readonly ApplicationDbContext _context;

    public EmailTemplateRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(EmailTemplate emailTemplate)
    {
        _context.EmailTemplates.Update(emailTemplate);
    }
}