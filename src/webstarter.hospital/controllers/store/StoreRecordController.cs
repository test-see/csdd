using csdd.Controllers.Shared;
using foundation.config;
using irespository.store.profile.model;
using iservice.store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webstarter.hospital.controllers.store
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class StoreRecordController : DefaultControllerBase
    {
        private readonly IStoreRecordService _storeRecordService;
        public StoreRecordController(IStoreRecordService storeRecordService)
        {
            _storeRecordService = storeRecordService;
        }

        [HttpPost]
        [Route("list")]
        public JsonResult GetList(PagerQuery<StoreRecordListQueryModel> query)
        {
            var data = _storeRecordService.GetPagerListAsync(query);
            return Json(data);
        }

    }
}
