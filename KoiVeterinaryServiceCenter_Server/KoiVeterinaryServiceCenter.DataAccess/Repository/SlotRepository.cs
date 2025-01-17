﻿using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository;

public class SlotRepository : Repository<Slot>,ISlotRepository
{
    private readonly ApplicationDbContext _context;
    
    public SlotRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Slot slot)
    {
        _context.Slots.Update(slot);
    }

    public void UpdateRange(IEnumerable<Slot> slots)
    {
        _context.Slots.UpdateRange(slots);
    }

    public async Task<Slot> GetSlotById(Guid slotId)
    {
        return await _context.Slots.FirstOrDefaultAsync(x => x.SlotId == slotId);
    }

    public async Task<IEnumerable<Slot>> GetAllSlotWithDoctor()
    {
        return await _context.Slots
        .Include(s => s.DoctorSchedules)
        .ThenInclude(ds => ds.Doctor)
        .ToListAsync();
    }
}