using AutoMapper;
using Doctor.Api.Models;
using Doctor.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Doctor.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public void Register([FromBody]User user)
        {
            _userService.CreateUser(_mapper.Map<Doctor.Entities.User>(user));
        }
    }
}
