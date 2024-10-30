using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository;

public class AppointmentPetRepository : Repository<AppointmentPet>, IAppointmentPetRepository
{
    private readonly ApplicationDbContext _context;
    
    public AppointmentPetRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public void Update(AppointmentPet appointmentPet)
    {
        _context.AppointmentPets.Update(appointmentPet);
    }

    public void UpdateRange(IEnumerable<AppointmentPet> appointmentPets)
    {
        _context.AppointmentPets.UpdateRange(appointmentPets);
    }

    public async Task<AppointmentPet> GetAppointmentPetById(Guid petId)
    {
        return await _context.AppointmentPets.FirstOrDefaultAsync(x => x.PetId == petId);
    }
}