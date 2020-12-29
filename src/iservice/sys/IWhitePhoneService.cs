using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;

namespace iservice.sys
{
    public interface IWhitePhoneService
    {
        PagerResult<WhitePhoneListApiModel> GetPagerList(PagerQuery<WhitePhoneListQueryModel> query);
        SysWhitePhone Create(WhitePhoneCreateApiModel created, int userId);
        int Delete(int id);
    }
}
