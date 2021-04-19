using domain.store;
using domain.store.enums;
using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.checklist;
using irespository.checklist.model;
using irespository.checklist.profile.enums;
using irespository.store.model;
using irespository.store.profile.model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace domain.checklist
{
    public class CheckListContext
    {
        private readonly ICheckListRespository _checkListRespository;
        private readonly ICheckListGoodsRespository _checkListGoodsRespository;
        private readonly StoreContext _storeContext;
        private readonly DefaultDbTransaction _defaultDbTransaction;
        public CheckListContext(ICheckListRespository CheckListRespository,
            StoreContext storeContext,
            DefaultDbTransaction defaultDbTransaction,
            ICheckListGoodsRespository checkListGoodsRespository)
        {
            _checkListRespository = CheckListRespository;
            _storeContext = storeContext;
            _defaultDbTransaction = defaultDbTransaction; 
            _checkListGoodsRespository = checkListGoodsRespository;
        }

        public async Task<PagerResult<CheckListApiModel>> GetPagerListAsync(PagerQuery<CheckListQueryModel> query, int hospitalId)
        {
            return await _checkListRespository.GetPagerListAsync(query, hospitalId);
        }
        public CheckList Create(CheckListCreateApiModel created, int userId)
        {
            return _checkListRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _checkListRespository.Delete(id);
        }
        public int Update(int id, CheckListUpdateApiModel updated)
        {
            return _checkListRespository.Update(id, updated);
        }
        public async Task<CheckListIndexApiModel> GetIndexAsync(int id)
        {
            var goods = await _checkListRespository.GetIndexAsync(id);
            return goods;
        }
        public int Submit(int id)
        {
            return _checkListRespository.UpdateStatus(id, CheckListStatus.Submited);
        }
        public async Task<int> BillAsync(int id, int userId)
        {
            var model = await _checkListRespository.GetIndexAsync(id);
            var list = await _checkListGoodsRespository.GetPreviewListAsync(id);
            using (var trans = _defaultDbTransaction.Begin())
            {
                var goods1 = list.Where(x => x.StoreQty > x.CheckQty).Select(x => new StoreChangeGoodsValueModel
                {
                    HospitalGoodId = x.HospitalGoods.Id,
                    ChangeQty = x.StoreQty - x.CheckQty,
                    Recrdno = RecordNumber.Next((int)StoreChangeType.CheckListOut, x.Id),
                });
                _storeContext.BatchCreateOrUpdate(new BatchStoreChangeApiModel
                {
                    ChangeTypeId = (int)StoreChangeType.CheckListOut,
                    HospitalChangeGoods = goods1.ToList(),
                }, model.HospitalDepartment.Id, userId);
                var goods2 = list.Where(x => x.StoreQty < x.CheckQty).Select(x => new StoreChangeGoodsValueModel
                {
                    HospitalGoodId = x.HospitalGoods.Id,
                    ChangeQty = -x.StoreQty + x.CheckQty,
                    Recrdno = RecordNumber.Next((int)StoreChangeType.CheckListIn, x.Id),
                });
                _storeContext.BatchCreateOrUpdate(new BatchStoreChangeApiModel
                {
                    ChangeTypeId = (int)StoreChangeType.CheckListIn,
                    HospitalChangeGoods = goods2.ToList(),
                }, model.HospitalDepartment.Id, userId);

                _checkListRespository.UpdateStatus(id, CheckListStatus.Billed);
                trans.Commit();
            }
            return 0;
        }
    }
}
