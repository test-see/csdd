using domain.store;
using foundation.config;
using foundation.ef5.poco;
using irespository.storeinout.model;
using iservice.store;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace service.store
{
    public class StoreInoutService : IStoreInoutService
    {
        private readonly StoreInoutContext _StoreInoutContext;
        public StoreInoutService(StoreInoutContext StoreInoutContext)
        {
            _StoreInoutContext = StoreInoutContext;
        }
        public async Task<PagerResult<StoreInoutListApiModel>> GetPagerListAsync(PagerQuery<StoreInoutListQueryModel> query)
        {
            return await _StoreInoutContext.GetPagerListAsync(query);
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
        public async Task<StoreInoutIndexApiModel> GetIndexAsync(int id)
        {
            return await _StoreInoutContext.GetIndexAsync(id);
        }

        public async Task<int> SubmitAsync(int id, int userId)
        {
            return await _StoreInoutContext.SubmitAsync(id, userId);
        }

        public IEnumerable<DataStoreChangeType> GetCustomizeChangeTypeList()
        {
            return _StoreInoutContext.GetCustomizeChangeTypeList();
        }
    }
}
