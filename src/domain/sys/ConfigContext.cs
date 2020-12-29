using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;
using irespository.user;

namespace domain.sys
{
    public class ConfigContext
    {
        private readonly ISysConfigRespository _sysConfigRespository;
        public ConfigContext(ISysConfigRespository sysConfigRespository)
        {
            _sysConfigRespository = sysConfigRespository;
        }

        public PagerResult<ConfigListApiModel> GetPagerList(PagerQuery<ConfigListQueryModel> query)
        {
            return _sysConfigRespository.GetPagerList(query);
        }
        public SysConfig Create(ConfigCreateApiModel created, int userId)
        {
            return _sysConfigRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _sysConfigRespository.Delete(id);
        }
    }
}
