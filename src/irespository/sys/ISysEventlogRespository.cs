using foundation.config;
using irespository.sys.model;

namespace irespository.sys
{
    public interface ISysEventlogRespository
    {
        PagerResult<EventlogListApiModel> GetPagerList(PagerQuery<EventlogListQueryModel> query);
    }
}
