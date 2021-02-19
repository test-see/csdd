using domain.store.enums;
using domain.storeinout;
using foundation.config;
using foundation.ef5.poco;
using irespository.store.inout.profile.enums;
using irespository.store.model;
using irespository.store.profile.model;
using irespository.storeinout;
using irespository.storeinout.model;
using System.Linq;

namespace domain.store
{
    public class StoreInoutContext
    {
        private readonly StoreContext _storeContext;
        private readonly IStoreInoutRespository _StoreInoutRespository;
        private readonly StoreInoutGoodsContext _storeInoutGoodsContext;
        public StoreInoutContext(IStoreInoutRespository StoreInoutRespository,
            StoreContext storeContext,
            StoreInoutGoodsContext storeInoutGoodsContext)
        {
            _StoreInoutRespository = StoreInoutRespository;
            _storeContext = storeContext;
            _storeInoutGoodsContext = storeInoutGoodsContext;
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
        public int Submit(int id, int userId)
        {
            var model = _StoreInoutRespository.Get(id);
            var list = _storeInoutGoodsContext.GetListByStoreInout(id);
            var goods = list.Select(x => new StoreChangeGoodsValueModel
            {
                HospitalGoodId = x.HospitalGoods.Id,
                Qty = x.Qty,
            });
            var success = _storeContext.BatchCreateOrUpdate(new BatchStoreChangeApiModel
            {
                ChangeTypeId = (int)StoreChangeType.CustomizeInout,
                HospitalChangeGoods = goods.ToList(),
            }, model.HospitalDepartmentId, userId);
            if (success) return _StoreInoutRespository.UpdateStatus(id, StoreInoutStatus.Submited);
            return 0;
        }
    }
}
