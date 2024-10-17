namespace KoiVeterinaryServiceCenter.Models.Domain;

public class EmailTemplate : BaseEntity<string, string, int>
{
    public Guid Id { get; set; }
    public string TemplateName { get; set; } = null!;
    public string? SenderName { get; set; }
    public string? SenderEmail { get; set; }
    public string Category { get; set; } = null!;
    public string SubjectLine { get; set; } = null!;
    public string? PreHeaderText { get; set; }
    public string? PersonalizationTags { get; set; }
    public string BodyContent { get; set; } = null!;
    public string? FooterContent { get; set; }
    public string? CallToAction { get; set; }
    public string? Language { get; set; }
    public string RecipientType { get; set; } = null!;
}