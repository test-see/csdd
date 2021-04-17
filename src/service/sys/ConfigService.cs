using domain.sys;
using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using iservice.sys;

namespace service.sys
{
    public class ConfigService : IConfigService
    {
        private readonly domain.sys.ConfigService _configContext;
        public ConfigService(domain.sys.ConfigService configContext)
        {
            _configContext = configContext;
        }
        public PagerResult<ListConfigResponse> GetPagerList(PagerQuery<ListConfigRequest> query)
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
