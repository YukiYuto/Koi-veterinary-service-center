using AutoMapper;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO.Doctor;
using KoiVeterinaryServiceCenter.Models.DTO.DoctorSchedules;
using KoiVeterinaryServiceCenter.Models.DTO.DoctorServices;

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
        .ForMember(dest => dest.DoctorServiceId, opt => opt.MapFrom(src => src.ServiceId))
        .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
        .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.ServiceName))
        .ForMember(dest => dest.DoctorFullName, opt => opt.MapFrom(src => src.Doctor.ApplicationUser.FullName))
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Service.Price))
        .ForMember(dest => dest.TravelFee, opt => opt.MapFrom(src => src.Service.TreavelFree))
        .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.Service.CreatedTime)).ReverseMap();
        CreateMap<DoctorSchedules, GetDoctorSchedulesDTO>()
            .ForMember(dest => dest.DoctorSchedulesId, opt => opt.MapFrom(src => src.DoctorSchedulesId))
            .ForMember(dest => dest.DoctorId, opt => opt.MapFrom((src) => src.DoctorId))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.ApplicationUser.FullName))
            .ForMember(dest => dest.SchedulesDate, opt => opt.MapFrom(src => src.SchedulesDate))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime)).ReverseMap();
    }
}