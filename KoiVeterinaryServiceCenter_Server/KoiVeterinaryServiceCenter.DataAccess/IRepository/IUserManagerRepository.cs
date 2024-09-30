﻿using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IUserManagerRepository
    {
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<bool> CheckIfPhoneNumberExistsAsync(string phoneNumber);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<ApplicationUser> FindByIdAsync(string userId);
        Task<ApplicationUser> FindByPhoneAsync(string phoneNumber);
        Task<List<ApplicationUser>> GetDoctorUser();
        Task<List<ApplicationUser>> GetCustomerUser();
        Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string role);
    }
}