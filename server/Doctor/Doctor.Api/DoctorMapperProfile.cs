using AutoMapper;
using Doctor.Database;
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
