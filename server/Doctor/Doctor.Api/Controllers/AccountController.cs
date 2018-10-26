﻿using AutoMapper;
using Doctor.Api.Authorization;
using Doctor.Api.Models;
using Doctor.BusinessLogic;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Doctor.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task RegisterAsync([FromBody]CreateUserModel user)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(user);
            var result = await _userManager.CreateAsync(applicationUser, user.Password);
        }
    }
}
