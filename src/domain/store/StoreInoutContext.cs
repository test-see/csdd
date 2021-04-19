using domain.storeinout;
using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.data;
using irespository.store.inout.profile.enums;
using irespository.store.model;
using irespository.store.profile.model;
using irespository.storeinout;
using irespository.storeinout.model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace domain.store
{
    public class StoreInoutContext
    {
        private readonly StoreContext _storeContext;
        private readonly IStoreInoutRespository _StoreInoutRespository;
        private readonly StoreInoutGoodsContext _storeInoutGoodsContext;
        private readonly DefaultDbTransaction _defaultDbTransaction;
        private readonly IStoreChangeTypeRespository _storeChangeTypeRespository;
        public StoreInoutContext(IStoreInoutRespository StoreInoutRespository,
            StoreContext storeContext,
            StoreInoutGoodsContext storeInoutGoodsContext,
            DefaultDbTransaction defaultDbTransaction,
            IStoreChangeTypeRespository storeChangeTypeRespository)
        {
            _StoreInoutRespository = StoreInoutRespository;
            _storeContext = storeContext;
            _storeInoutGoodsContext = storeInoutGoodsContext;
            _defaultDbTransaction = defaultDbTransaction;
            _storeChangeTypeRespository = storeChangeTypeRespository;
        }

        public async Task<PagerResult<StoreInoutListApiModel>> GetPagerListAsync(PagerQuery<StoreInoutListQueryModel> query)
        {
            return await _StoreInoutRespository.GetPagerListAsync(query);
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
        public async Task<StoreInoutIndexApiModel> GetIndexAsync(int id)
        {
            return await _StoreInoutRespository.GetIndexAsync(id);
        }


        public async Task<int> SubmitAsync(int id, int userId)
        {
            var model = await GetIndexAsync(id);
            var list = await _storeInoutGoodsContext.GetListByStoreInoutAsync(id);
            var goods = list.Select(x => new StoreChangeGoodsValueModel
            {
                HospitalGoodId = x.HospitalGoods.Id,
                ChangeQty = x.Qty,
                Recrdno = RecordNumber.Next(model.ChangeType.Id, x.Id),
            });
            using (var trans = _defaultDbTransaction.Begin())
            {
                _storeContext.BatchCreateOrUpdate(new BatchStoreChangeApiModel
                {
                    ChangeTypeId = model.ChangeType.Id,
                    HospitalChangeGoods = goods.ToList(),
                }, model.HospitalDepartment.Id, userId);
                _StoreInoutRespository.UpdateStatus(id, StoreInoutStatus.Submited);
                trans.Commit();
            }
            return 0;
        }
        public IEnumerable<DataStoreChangeType> GetCustomizeChangeTypeList()
        {
            return _storeChangeTypeRespository.GetCustomizeList();
        }
    }
}
