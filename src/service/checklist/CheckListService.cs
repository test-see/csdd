using domain.checklist;
using domain.store;
using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.goods.model;
using irespository.checklist.model;
using irespository.checklist.profile.model;
using iservice.checklist;
using System.Threading.Tasks;

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
        public async Task<PagerResult<CheckListApiModel>> GetPagerListAsync(PagerQuery<CheckListQueryModel> query, int hospitalId)
        {
            return await _CheckListContext.GetPagerListAsync(query, hospitalId);
        }
        public async Task<CheckListPreviewApiModel> GetPagerPreviewListAsync(int checkListId, PagerQuery<CheckListGoodsPreviewQueryModel> query)
        {
            return new CheckListPreviewApiModel
            {
                CheckListGoods = await _CheckListGoodsContext.GetPagerPreviewListAsync(checkListId, query),
                Amount = _CheckListGoodsContext.GetPreviewListAmount(checkListId),
            };
        }
        public async Task<CheckList> CreateAsync(CheckListCreateApiModel created, int userId)
        {
            var check = _CheckListContext.Create(created, userId);
            var stores = await _storeContext.GetListByDepartmentAsync(created.HospitalDepartmentId);
            foreach (var s in stores)
            {
                _CheckListGoodsContext.Create(new CheckListGoodsCreateApiModel
                {
                    CheckListId = check.Id,
                    CheckQty = s.Qty,
                    StoreQty = s.Qty,
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

        public async Task<CheckListIndexApiModel> GetIndexAsync(int id)
        {
            return await _CheckListContext.GetIndexAsync(id);
        }

        public int Submit(int id)
        {
            return _CheckListContext.Submit(id);
        }
        public async Task<int> BillAsync(int id, int userId)
        {
            return await _CheckListContext.BillAsync(id, userId);
        }
    }
}
