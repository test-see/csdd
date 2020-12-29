using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;

namespace iservice.sys
{
    public interface IConfigService
    {
        PagerResult<ConfigListApiModel> GetPagerList(PagerQuery<ConfigListQueryModel> query);
        SysConfig Create(ConfigCreateApiModel created, int userId);
        int Delete(int id);
    }
}
