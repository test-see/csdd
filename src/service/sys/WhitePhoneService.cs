using domain.sys;
using foundation.config;
using irespository.sys.model;
using iservice.sys;

namespace service.sys
{
    public class WhitePhoneService: IWhitePhoneService
    {
        private readonly WhitePhoneContext _whitePhoneContext;
        public WhitePhoneService(WhitePhoneContext whitePhoneContext)
        {
            _whitePhoneContext = whitePhoneContext;
        }
        public PagerResult<WhitePhoneListApiModel> GetPagerList(PagerQuery<WhitePhoneListQueryModel> query)
        {
            return _whitePhoneContext.GetPagerList(query);
        }
    }
}
