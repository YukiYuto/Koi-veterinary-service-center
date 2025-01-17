﻿using KoiVeterinaryServiceCenter.Services.IRepositories;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface IUnitOfWork
{
    IDoctorRepository DoctorRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    ISlotRepository SlotRepository { get; }
    IAppointmentRepository AppointmentRepository { get; }
    IAppointmentDepositRepository AppointmentDepositRepository { get; }
    IDoctorSchedulesRepository DoctorSchedulesRepository { get; }
    IEmailTemplateRepository EmailTemplateRepository { get; }
    IServiceRepository ServiceRepository { get; }
    IDoctorServicesRepository DoctorServicesRepository { get; }

    IDoctorRatingRepository DoctorRatingRepository { get; }

    IPostRepository PostRepository { get; }

    IPaymentTransactionsRepository PaymentTransactionsRepository { get; }
    ITransactionsRepository TransactionsRepository { get; }

    IPoolRepository PoolRepository { get; }
    IPetServiceRepository PetServiceRepository { get; }

    IPetRepository PetRepository { get; }

    IPetDiseaseRepository PetDiseaseRepository { get; }

    IDiseaseRepository diseaseRepository { get; }


    Task<int> SaveAsync();
}