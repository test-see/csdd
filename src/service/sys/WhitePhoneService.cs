using domain.sys;
using foundation.config;
using foundation.ef5.poco;
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
        public SysWhitePhone Create(WhitePhoneCreateApiModel created, int userId)
        {
            return _whitePhoneContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _whitePhoneContext.Delete(id);
        }
    }
}
