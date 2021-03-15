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
using System.Linq;

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

        public PagerResult<CheckListApiModel> GetPagerList(PagerQuery<CheckListQueryModel> query, int hospitalId)
        {
            return _checkListRespository.GetPagerList(query, hospitalId);
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
        public CheckListIndexApiModel GetIndex(int id)
        {
            var goods = _checkListRespository.GetIndex(id);
            return goods;
        }
        public int Submit(int id)
        {
            return _checkListRespository.UpdateStatus(id, CheckListStatus.Submited);
        }
        public int Bill(int id, int userId)
        {
            var model = _checkListRespository.GetIndex(id);
            var list = _checkListGoodsRespository.GetPreviewList(id);
            using (var trans = _defaultDbTransaction.Begin())
            {
                var goods1 = list.Where(x => x.StoreQty > x.CheckQty).Select(x => new StoreChangeGoodsValueModel
                {
                    HospitalGoodId = x.HospitalGoods.Id,
                    Qty = x.StoreQty - x.CheckQty
                });
                _storeContext.BatchCreateOrUpdate(new BatchStoreChangeApiModel
                {
                    ChangeTypeId = (int)StoreChangeType.CheckListOut,
                    HospitalChangeGoods = goods1.ToList(),
                }, model.HospitalDepartment.Id, userId);
                var goods2 = list.Where(x => x.StoreQty < x.CheckQty).Select(x => new StoreChangeGoodsValueModel
                {
                    HospitalGoodId = x.HospitalGoods.Id,
                    Qty = -x.StoreQty + x.CheckQty,
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
