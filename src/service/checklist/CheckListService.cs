using domain.checklist;
using domain.store;
using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.goods.model;
using irespository.checklist.model;
using irespository.checklist.profile.model;
using iservice.checklist;

namespace service.checklist
{
    public class CheckListService : ICheckListService
    {
        private readonly CheckListContext _CheckListContext;
        private readonly CheckListGoodsContext _CheckListGoodsContext;
        private readonly StoreContext _storeContext;
        public CheckListService(CheckListContext CheckListContext,
            CheckListGoodsContext CheckListGoodsContext,
            StoreContext storeContext)
        {
            _CheckListContext = CheckListContext;
            _CheckListGoodsContext = CheckListGoodsContext;
            _storeContext = storeContext;
        }
        public PagerResult<CheckListApiModel> GetPagerList(PagerQuery<CheckListQueryModel> query, int hospitalId)
        {
            return _CheckListContext.GetPagerList(query, hospitalId);
        }
        public CheckListPreviewApiModel GetPagerPreviewList(int checkListId, PagerQuery<CheckListGoodsPreviewQueryModel> query)
        {
            return new CheckListPreviewApiModel
            {
                CheckListGoods = _CheckListGoodsContext.GetPagerPreviewList(checkListId, query),
                Amount = _CheckListGoodsContext.GetPreviewListAmount(checkListId),
            };
        }
        public CheckList Create(CheckListCreateApiModel created, int userId)
        {
            var check = _CheckListContext.Create(created, userId);
            var stores = _storeContext.GetListByDepartment(created.HospitalDepartmentId);
            foreach (var s in stores)
            {
                _CheckListGoodsContext.Create(new CheckListGoodsCreateApiModel
                {
                    CheckListId = check.Id,
                    CheckQty = s.Qty,
                    HospitalGoodsId = s.HospitalGoods.Id,
                }, userId);
            }
            return check;
        }

        public int Delete(int id)
        {
            return _CheckListContext.Delete(id);
        }

        public int Update(int id, CheckListUpdateApiModel updated)
        {
            return _CheckListContext.Update(id, updated);
        }

        public CheckListIndexApiModel GetIndex(int id)
        {
            return _CheckListContext.GetIndex(id);
        }

        public int Submit(int id)
        {
            return _CheckListContext.Submit(id);
        }
        public int Bill(int id, int userId)
        {
            return _CheckListContext.Bill(id, userId);
        }
    }
}
