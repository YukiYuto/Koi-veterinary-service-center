using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface ISlotRepository : IRepository<Slot>
{
    void Update(Slot slot);
    void UpdateRange(IEnumerable<Slot> slots);
    Task<Slot> GetSlotById(Guid slotId);
}