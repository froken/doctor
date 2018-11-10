using AutoMapper;
using Doctor.Database;
using Doctor.Server.Models;

namespace Doctor.Server
{
    public class DoctorMapperProfile : Profile
    {
        public DoctorMapperProfile()
        {
            CreateMap<CreateUserModel, ApplicationUser>();
        }
    }
}
