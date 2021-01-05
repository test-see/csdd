using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;

namespace irespository.user
{
    public interface ISysWhitePhoneRespository
    {
        bool Exists(string phone);
        PagerResult<WhitePhoneListApiModel> GetPagerList(PagerQuery<WhitePhoneListQueryModel> query);
        SysWhitePhone Create(WhitePhoneCreateApiModel created, int userId);
        int Delete(int id);
    }
}
