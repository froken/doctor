using AutoMapper;
using Doctor.Api.Authorization;
using Doctor.Api.Models;

namespace Doctor.Api
{
    public class DoctorMapperProfile : Profile
    {
        public DoctorMapperProfile()
        {
            CreateMap<CreateUserModel, ApplicationUser>();
        }
    }
}
