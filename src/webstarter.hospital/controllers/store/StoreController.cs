using csdd.Controllers.Shared;
using foundation.config;
using irespository.store.model;
using irespository.store.profile.model;
using iservice.store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<JsonResult> GetListAsync(PagerQuery<StoreListQueryModel> query)
        {
            var data = await _storeService.GetPagerListAsync(query);
            return Json(data);
        }

        [HttpGet]
        [Route("index")]
        public JsonResult GetIndexByGoods(int goodid)
        {
            var data = _storeService.GetIndexByGoods(HospitalDepartment.Id, goodid);
            return Json(data);
        }
    }
}
