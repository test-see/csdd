using foundation.config;
using foundation.ef5.poco;
using irespository.sys.model;

namespace irespository.user
{
    public interface ISysConfigRespository
    {
        PagerResult<ConfigListApiModel> GetPagerList(PagerQuery<ConfigListQueryModel> query);
        SysConfig Create(ConfigCreateApiModel created, int userId);
        int Delete(int id);
    }
}
