using foundation.config;
using irespository.sys.model;

namespace iservice.sys
{
    public interface IWhitePhoneService
    {
        PagerResult<WhitePhoneListApiModel> GetPagerList(PagerQuery<WhitePhoneListQueryModel> query);
    }
}
