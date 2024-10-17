﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IUnitOfWork
    {
        IDoctorRepository DoctorRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ISlotRepository SlotRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
        IDoctorSchedulesRepository DoctorSchedulesRepository { get; }
        IEmailTemplateRepository EmailTemplateRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IDoctorServicesRepository DoctorServicesRepository { get; }
        IPaymentTransactionsRepository PaymentTransactionsRepository { get; }
        IOrderItemsRepository OrderItemsRepository { get; }
        Task<int> SaveAsync();
    }
}