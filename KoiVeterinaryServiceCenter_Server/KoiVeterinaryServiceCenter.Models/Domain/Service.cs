using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class Service : BaseEntity<string, string, int>
{
    [Key] public Guid ServiceId { get; set; }
    public string ServiceName { get; set; } = null!;
    public long ServiceNumber { get; set; }
    public double Price { get; set; }
    public double TreavelFree { get; set; }

    [NotMapped] public virtual ICollection<DoctorServices> DoctorServices { get; set; } = null!;
}