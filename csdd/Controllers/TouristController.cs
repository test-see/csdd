using csdd.Controllers.Shared;
using iservice.tourist;

namespace csdd.Controllers
{
    public class TouristController : DefaultControllerBase
    {
        private readonly ITouristService _touristService;
        public TouristController(ITouristService touristService)
        {
            _touristService = touristService;
        }

    }
}
