using foundation.config;
using irespository.sys.model;
using irespository.user;

namespace domain.sys
{
    public class WhitePhoneContext
    {
        private readonly ISysWhitePhoneRespository _sysWhitePhoneRespository;
        public WhitePhoneContext(ISysWhitePhoneRespository sysWhitePhoneRespository)
        {
            _sysWhitePhoneRespository = sysWhitePhoneRespository;
        }

        public PagerResult<WhitePhoneListApiModel> GetPagerList(PagerQuery<WhitePhoneListQueryModel> query)
        {
            return _sysWhitePhoneRespository.GetPagerList(query);
        }
    }
}
