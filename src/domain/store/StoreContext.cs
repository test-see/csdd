using foundation.config;
using foundation.ef5;
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
        private readonly DefaultDbTransaction _defaultDbTransaction;
        public StoreContext(IStoreRespository storeRespository,
            IStoreChangeTypeRespository storeChangeTypeRespository,
            DefaultDbTransaction defaultDbTransaction)
        {
            _storeRespository = storeRespository;
            _storeChangeTypeRespository = storeChangeTypeRespository;
            _defaultDbTransaction = defaultDbTransaction;
        }

        public PagerResult<StoreListApiModel> GetPagerList(PagerQuery<StoreListQueryModel> query)
        {
            return _storeRespository.GetPagerList(query);
        }

        public int BatchCreateOrUpdate(BatchStoreChangeApiModel created, int department, int userId)
        {
            lock (balance)
            {
                using (var trans = _defaultDbTransaction.Begin())
                {
                    var changetype = _storeChangeTypeRespository.GetIndex(created.ChangeTypeId);
                    foreach (var item in created.HospitalChangeGoods)
                    {
                        var store = _storeRespository.GetIndexByGoods(department, item.HospitalGoodId);
                        var afterqty = (store?.Qty ?? 0) + changetype.Operator * item.Qty;
                        if (afterqty < 0)
                            throw new DefaultException("库存不足!");
                        _storeRespository.CreateOrUpdate(item, created.ChangeTypeId, department, userId);
                    }
                    trans.Commit();
                }
            }
            return created.HospitalChangeGoods.Count;
        }

        public int CreateOrUpdate(StoreChangeApiModel created, int department, int userId)
        {
            var changetype = _storeChangeTypeRespository.GetIndex(created.ChangeTypeId);
            lock (balance)
            {
                using (var trans = _defaultDbTransaction.Begin())
                {
                    var store = _storeRespository.GetIndexByGoods(department, created.HospitalChangeGoods.HospitalGoodId);
                    var afterqty = (store?.Qty ?? 0) + changetype.Operator * created.HospitalChangeGoods.Qty;
                    if (afterqty < 0)
                        throw new DefaultException("库存不足!");
                    var id = _storeRespository.CreateOrUpdate(created.HospitalChangeGoods, created.ChangeTypeId, department, userId);
                    trans.Commit();
                    return id;
                }
            }
        }

        public Store GetIndexByGoods(int department, int goods)
        {
            return _storeRespository.GetIndexByGoods(department, goods);
        }
    }
}
