﻿using csdd.Controllers.Shared;
using iservice.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace csdd.Controllers
{
    public class UserController : DefaultControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("verification/generate")]
        [AllowAnonymous]
        public async Task<string> GenerateVerificationCodeAsync(string phone)
        {
            return await _userService.GenerateVerificationCodeAsync(phone);
        }
    }
}
