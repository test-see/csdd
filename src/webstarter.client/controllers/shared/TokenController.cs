using foundation.config;
using irespository.user;
using iservice.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace csdd.Controllers.Shared
{
    public class TokenController : DefaultControllerBase
    {
        private readonly ITokenService _userService;
        private readonly AppConfig _appConfig;
        public TokenController(ITokenService userService,
            AppConfig appConfig)
        {
            _userService = userService;
            _appConfig = appConfig;
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Post(LoginApiModel login)
        {
            var profile = _userService.LoginByHospital(login);

            var identity = new ClaimsIdentity();
            var key = Encoding.UTF8.GetBytes(_appConfig.Authentication.IssuerSigningKey);
            identity.AddClaim(new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(profile.User)));
            identity.AddClaim(new Claim(ClaimTypes.Role, profile.AuthorizeRoleId.ToString()));
            identity.AddClaim(new Claim("HospitalDepartment", JsonConvert.SerializeObject(profile.HospitalDepartment)));

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _appConfig.Authentication.ValidAudience,
                Issuer = _appConfig.Authentication.ValidIssuer
            };
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(descriptor);
            return Json(handler.WriteToken(token));
        }

        [HttpGet]
        [Route("verification/generate")]
        [AllowAnonymous]
        public async Task<JsonResult> GenerateVerificationCodeAsync(string phone)
        {
            var data = await _userService.GenerateVerificationCodeAsync(phone);
            return Json(data);
        }
    }
}
