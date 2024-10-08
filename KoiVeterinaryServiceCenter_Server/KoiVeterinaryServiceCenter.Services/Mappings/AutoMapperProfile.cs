using AutoMapper;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserInfoDTO, ApplicationUser>().ReverseMap();
            CreateMap<Slot, GetSlotDTO>().ReverseMap();
            CreateMap<Appointment, GetAppointmentDTO>().ReverseMap();
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
            CreateMap<Service, GetServiceDTO>().ReverseMap();
            CreateMap<DoctorServices, GetDoctorServicesDTO>()
            .ForMember(dest => dest.DoctorServiceId, opt => opt.MapFrom(src => src.ServiceId))
            .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.ServiceName))
            .ForMember(dest => dest.DoctorFullName, opt => opt.MapFrom(src => src.Doctor.ApplicationUser.FullName))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Service.Price))
            .ForMember(dest => dest.TravelFee, opt => opt.MapFrom(src => src.Service.TreavelFree))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.Service.CreatedTime)).ReverseMap();
        }
    }
}
