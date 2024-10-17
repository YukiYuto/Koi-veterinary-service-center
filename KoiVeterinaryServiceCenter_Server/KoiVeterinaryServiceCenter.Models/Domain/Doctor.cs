using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class Doctor
{
    [Key] public Guid DoctorId { get; set; }
    public string UserId { get; set; } = null!;
    [ForeignKey("UserId")] public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public string Experience{ get; set; } = null!;
    public string Degree { get; set; } = null!;

    [NotMapped]public virtual ICollection<DoctorServices> DoctorServices { get; set; }
}