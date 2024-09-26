﻿using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IDoctorRepository DoctorRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IRefreshTokenRepository RefreshTokens { get; set; }
        public ISlotRepository SlotRepository { get; set; }
        public IAppointmentRepository AppointmentRepository { get; set; }


        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            DoctorRepository = new DoctorRepository(_context);
            CustomerRepository = new CustomerRepository(_context);
            RefreshTokens = new RefreshTokenRepository(_context);
            SlotRepository = new SlotRepository(_context);
            AppointmentRepository = new AppointmentRepository(_context);

        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
