using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout;
using irespository.storeinout.model;

namespace domain.store
{
    public class StoreInoutContext
    {
        private readonly IStoreInoutRespository _StoreInoutRespository;
        public StoreInoutContext(IStoreInoutRespository StoreInoutRespository)
        {
            _StoreInoutRespository = StoreInoutRespository;
        }

        public PagerResult<StoreInoutListApiModel> GetPagerList(PagerQuery<StoreInoutListQueryModel> query)
        {
            return _StoreInoutRespository.GetPagerList(query);
        }
        public StoreInout Create(StoreInoutCreateApiModel created, int departmentId, int userId)
        {
            return _StoreInoutRespository.Create(created, departmentId, userId);
        }
        public int Delete(int id)
        {
            return _StoreInoutRespository.Delete(id);
        }
        public int Update(int id, StoreInoutUpdateApiModel updated)
        {
            return _StoreInoutRespository.Update(id, updated);
        }
    }
}
