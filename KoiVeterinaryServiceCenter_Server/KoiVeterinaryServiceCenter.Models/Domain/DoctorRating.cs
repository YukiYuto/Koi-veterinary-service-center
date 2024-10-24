using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class DoctorRating : BaseEntity<string, string, int>
{
    [Key]public Guid DoctorRatingId { get; set; }
    public Guid DoctorId { get; set; }
    [ForeignKey("DoctorId")] public virtual Doctor Doctor { get; set; } = null!;

    [Range(1, 5)]
    public int Rating { get; set; }
    public string? Feedback { get; set; }
}