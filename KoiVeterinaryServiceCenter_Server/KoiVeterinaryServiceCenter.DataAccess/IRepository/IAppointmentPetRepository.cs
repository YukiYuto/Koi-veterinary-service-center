using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface IAppointmentPetRepository :IRepository<AppointmentPet>
{
    void Update(AppointmentPet appointmentPet);
    void UpdateRange(IEnumerable<AppointmentPet> appointmentPets);
    Task<AppointmentPet> GetAppointmentPetById(Guid petId);
}