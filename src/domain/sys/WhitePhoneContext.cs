using foundation.config;
using foundation.ef5.poco;
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
        public SysWhitePhone Create(WhitePhoneCreateApiModel created, int userId)
        {
            return _sysWhitePhoneRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _sysWhitePhoneRespository.Delete(id);
        }
    }
}
