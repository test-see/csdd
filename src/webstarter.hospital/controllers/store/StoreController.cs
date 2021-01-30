using csdd.Controllers.Shared;
using foundation.config;
using irespository.store.model;
using irespository.store.profile.model;
using iservice.store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.store
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class StoreController : DefaultControllerBase
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<StoreListQueryModel> query)
        {
            var data = _storeService.GetPagerList(query);
            return Json(data);
        }

        [HttpPost]
        [Route("add")]
        public JsonResult Post(CustomizeStoreChangeApiModel created)
        {
            var data = _storeService.CustomizeCreate(created, HospitalDepartment.Id, Profile.Id);
            return Json(data);
        }
    }
}
