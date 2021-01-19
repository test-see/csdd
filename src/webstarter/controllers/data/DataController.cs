using csdd.Controllers.Shared;
using iservice.data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csdd.controllers.data
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class DataController : DefaultControllerBase
    {
        private readonly IDataService _dataService;
        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet]
        [Route("authorize")]
        public JsonResult GetDataAuthorizeList()
        {
            var data = _dataService.GetDataAuthorizeList();
            return Json(data);
        }
    }
}
