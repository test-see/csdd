using csdd.Controllers.Shared;
using foundation.config;
using irespository.storeinout.model;
using iservice.store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace webstarter.hospital.controllers.StoreInout
{
    [Authorize(Policy = "RequireHospitalRole")]
    public class StoreInoutGoodsController : DefaultControllerBase
    {
        private readonly IStoreInoutGoodsService _StoreInoutGoodsService;
        public StoreInoutGoodsController(IStoreInoutGoodsService StoreInoutGoodsService)
        {
            _StoreInoutGoodsService = StoreInoutGoodsService;
        }

        [HttpPost]
        [Route("list")]
        public async Task<JsonResult> GetListAsync(PagerQuery<StoreInoutGoodsListQueryModel> query)
        {
            var data = await _StoreInoutGoodsService.GetPagerListAsync(query);
            return Json(data);
        }


        [HttpGet]
        [Route("{id}/delete")]
        public JsonResult Delete(int id)
        {
            var data = _StoreInoutGoodsService.Delete(id);
            return Json(data);
        }


        [HttpPost]
        [Route("add")]
        public JsonResult Post(StoreInoutGoodsCreateApiModel created)
        {
            var data = _StoreInoutGoodsService.Create(created, Profile.Id);
            return Json(data);
        }

        [HttpPost]
        [Route("{id}/update")]
        public JsonResult Update(int id, StoreInoutGoodsUpdateApiModel updated)
        {
            var data = _StoreInoutGoodsService.Update(id, updated);
            return Json(data);
        }
    }
}
