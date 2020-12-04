using csdd.Controllers.Shared;
using foundation.config;
using irespository.tourist.model;
using iservice.data;
using iservice.tourist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet]
        [Route("register/steptwo/{provinceId}/hospital")]
        [AllowAnonymous]
        public OkMessage GetStepTowHospitals(int provinceId)
        {
            var data = _touristService.GetHospitals(provinceId);
            return new OkMessage(data);
        }

        [HttpGet]
        [Route("register/steptwo/{provinceId}/client")]
        [AllowAnonymous]
        public OkMessage GetStepTowClients(int provinceId)
        {
            var data = _touristService.GetClients(provinceId);
            return new OkMessage(data);
        }

        [HttpGet]
        [Route("register/steptwo/{hospitalId}/department")]
        [AllowAnonymous]
        public OkMessage GetStepTowDepartments(int hospitalId)
        {
            var data = _touristService.GetHospitalDepartments(hospitalId);
            return new OkMessage(data);
        }

        [HttpGet]
        [Route("register/steptwo/{hospitalId}/goods")]
        [AllowAnonymous]
        public OkMessage GetStepTowGoods(int hospitalId, string name)
        {
            var data = _touristService.GetHospitalGoods(hospitalId, name);
            return new OkMessage(data);
        }

        [HttpPost]
        [Route("register/tourist")]
        [AllowAnonymous]
        public async Task<OkMessage> CreateTouristAsync(TouristRegisterApiModel tourist)
        {
            var data = await _touristService.CreateTouristAsync(tourist);
            return new OkMessage(data);
        }
    }
}
