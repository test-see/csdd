using domain.sys;
using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using iservice.sys;

namespace service.sys
{
    public class ConfigService : IConfigService
    {
        private readonly ConfigContext _configContext;
        public ConfigService(ConfigContext configContext)
        {
            _configContext = configContext;
        }
        public PagerResult<ConfigListApiModel> GetPagerList(PagerQuery<ConfigListQueryModel> query)
        {
            return _configContext.GetPagerList(query);
        }
        public SysConfig Create(ConfigCreateApiModel created, int userId)
        {
            return _configContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _configContext.Delete(id);
        }
    }
}
