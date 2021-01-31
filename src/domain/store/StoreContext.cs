using foundation.config;
using foundation.ef5.poco;
using foundation.exception;
using irespository.data;
using irespository.purchase.model;
using irespository.store;
using irespository.store.model;
using irespository.store.profile.model;
using System.Collections.Generic;

namespace domain.store
{
    public class StoreContext
    {
        private readonly object balance = new object();
        private readonly IStoreRespository _storeRespository;
        private readonly IStoreChangeTypeRespository _storeChangeTypeRespository;
        public StoreContext(IStoreRespository storeRespository,
            IStoreChangeTypeRespository storeChangeTypeRespository)
        {
            _storeRespository = storeRespository;
            _storeChangeTypeRespository = storeChangeTypeRespository;
        }

        public PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query)
        {
            return _storeRespository.GetPagerList(query);
        }

        public Store CustomizeCreate(CustomizeStoreChangeApiModel created, int department, int userId)
        {
            var changetype = _storeChangeTypeRespository.GetIndex(created.ChangeTypeId);
            lock (balance)
            {
                var store = _storeRespository.GetIndexByGoods(department, created.HospitalGoodsId);
                var afterqty = (store?.Qty ?? 0) + changetype.Operator * created.ChangeQty;
                if (afterqty < 0)
                    throw new DefaultException("库存不足!");
                var updated = new StoreUpdateApiModel
                {
                    ChangeTypeId = changetype.Id,
                    Qty = afterqty,
                    HospitalGoodsId = created.HospitalGoodsId,
                };
                return _storeRespository.CreateOrUpdate(updated, department, userId);
            }
        }

        public IEnumerable<DataStoreChangeType> GetCustomizeChangeTypeList()
        {
            return _storeChangeTypeRespository.GetCustomizeList();
        }


        public Store GetIndexByGoods(int department, int goods)
        {
            return _storeRespository.GetIndexByGoods(department, goods);
        }
    }
}
