using foundation.config;
using irespository.user;
using iservice.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace csdd.Controllers.Shared
{
    public class TokenController : DefaultControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppConfig _appConfig;
        public TokenController(IUserService userService,
            AppConfig appConfig)
        {
            _userService = userService;
            _appConfig = appConfig;
        }
        [HttpPost]
        [AllowAnonymous]
        public string Post(LoginOrRegisterApiModel login)
        {
            var user = _userService.LoginOrRegister(login);
            var identity = new ClaimsIdentity();
            var key = Encoding.UTF8.GetBytes(_appConfig.Authentication.IssuerSigningKey);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _appConfig.Authentication.ValidAudience,
                Issuer = _appConfig.Authentication.ValidIssuer
            };
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
