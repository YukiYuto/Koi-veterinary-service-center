using AutoMapper;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO.Service;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO.Appointment;
using KoiVeterinaryServiceCenter.Models.DTO.Doctor;
using KoiVeterinaryServiceCenter.Models.DTO.DoctorSchedules;
using KoiVeterinaryServiceCenter.Models.DTO.DoctorServices;
using KoiVeterinaryServiceCenter.Models.DTO.Pool;
using KoiVeterinaryServiceCenter.Models.DTO.Transaction;

namespace KoiVeterinaryServiceCenter.Services.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserInfoDTO, ApplicationUser>().ReverseMap();
        CreateMap<DoctorInfoDTO, ApplicationUser>().ReverseMap();
        CreateMap<Doctor, DoctorInfoDTO>()
        .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
        .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.ApplicationUser.FullName))
        .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.ApplicationUser.Gender))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber))
        .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.ApplicationUser.BirthDate))
        .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.ApplicationUser.Country))
        .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization))
        .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Degree))
        .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience)).ReverseMap();
        CreateMap<DoctorServices, GetDoctorServicesDTO>()
        .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
        .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.ServiceName))
        .ForMember(dest => dest.DoctorFullName, opt => opt.MapFrom(src => src.Doctor.ApplicationUser.FullName))
        .ForMember(dest => dest.DoctorUrl, opt => opt.MapFrom(src => src.Doctor.ApplicationUser.AvatarUrl))
        .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Doctor.Specialization))
        .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Doctor.Experience))
        .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Doctor.Degree))
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Service.Price))
        .ForMember(dest => dest.TravelFee, opt => opt.MapFrom(src => src.Service.TreavelFree))
        .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.Service.CreatedTime)).ReverseMap();
        CreateMap<DoctorSchedules, GetDoctorSchedulesDTO>().ReverseMap();
        CreateMap<Service, GetServiceDTO>().ReverseMap();
        CreateMap<DoctorSchedules, GetDoctorSchedulesIdDTO>().ReverseMap();
        CreateMap<Transaction, GetTransactionDTO>().ReverseMap();
        CreateMap<Appointment, CreateAppointmentDTO>().ReverseMap();
        CreateMap<Appointment, GetAppointmentDTO>().ReverseMap();
        CreateMap<Transaction, GetFullInforTransactionDTO>()
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Appointment.Slot.DoctorSchedules.Doctor.ApplicationUser.FullName))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Appointment.Slot.DoctorSchedules.Doctor.Position))
            .ForMember(dest => dest.DoctorAvatarUrl, opt => opt.MapFrom(src => src.Appointment.Slot.DoctorSchedules.Doctor.ApplicationUser.AvatarUrl))
            .ForMember(dest => dest.NameService, opt => opt.MapFrom(src => src.Appointment.Service.ServiceName))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Appointment.Service.Price))
            .ForMember(dest => dest.TravelFee, opt => opt.MapFrom(src => src.Appointment.Service.TreavelFree))
            .ForMember(dest => dest.ScheduleDate, opt => opt.MapFrom(src => src.Appointment.Slot.SchedulesDate))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Appointment.Slot.StartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Appointment.Slot.EndTime))
            .ForMember(dest => dest.TransactionDateTime, opt => opt.MapFrom(src => src.TransactionDateTime)).ReverseMap();
        CreateMap<Pool, GetPoolFullInfo>()
            .ForMember(dest => dest.PoolName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Customer.Gender))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Customer.PhoneNumber)).ReverseMap();
    }
}