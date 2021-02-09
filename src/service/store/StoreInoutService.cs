using domain.store;
using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout.model;
using iservice.store;

namespace service.store
{
    public class StoreInoutService : IStoreInoutService
    {
        private readonly StoreInoutContext _StoreInoutContext;
        public StoreInoutService(StoreInoutContext StoreInoutContext)
        {
            _StoreInoutContext = StoreInoutContext;
        }
        public PagerResult<StoreInoutListApiModel> GetPagerList(PagerQuery<StoreInoutListQueryModel> query)
        {
            return _StoreInoutContext.GetPagerList(query);
        }
        public StoreInout Create(StoreInoutCreateApiModel created, int departmentId, int userId)
        {
            return _StoreInoutContext.Create(created, departmentId, userId);
        }

        public int Delete(int id)
        {
            return _StoreInoutContext.Delete(id);
        }

        public int Update(int id, StoreInoutUpdateApiModel updated)
        {
            return _StoreInoutContext.Update(id, updated);
        }
    }
}
