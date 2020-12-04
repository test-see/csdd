using csdd.Controllers.Shared;
using foundation.config;
using iservice.data;
using iservice.data.model;
using iservice.tourist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.Controllers
{
    public class TouristController : DefaultControllerBase
    {
        private readonly ITouristService _touristService;
        private readonly IDataTouristRegisterService _dataTouristRegisterService;
        public TouristController(ITouristService touristService,
            IDataTouristRegisterService dataTouristRegisterService)
        {
            _touristService = touristService;
            _dataTouristRegisterService = dataTouristRegisterService;
        }

        [HttpGet]
        [Route("register/stepone/data")]
        [AllowAnonymous]
        public OkMessage GetStepOneData()
        {
            var data = _dataTouristRegisterService.GetStepOneData();
            return new OkMessage(data);
        }
    }
}
