using domain.checklist;
using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.goods.model;
using irespository.checklist.model;
using iservice.checklist;

namespace service.checklist
{
    public class CheckListService : ICheckListService
    {
        private readonly CheckListContext _CheckListContext;
        private readonly CheckListGoodsContext _CheckListGoodsContext;
        public CheckListService(CheckListContext CheckListContext,
            CheckListGoodsContext CheckListGoodsContext)
        {
            _CheckListContext = CheckListContext;
            _CheckListGoodsContext = CheckListGoodsContext;
        }
        public PagerResult<CheckListApiModel> GetPagerList(PagerQuery<CheckListQueryModel> query)
        {
            return _CheckListContext.GetPagerList(query);
        }
        public PagerResult<CheckListGoodsPreviewListApiModel> GetPagerPreviewList(int checkListId, PagerQuery<CheckListGoodsPreviewQueryModel> query)
        {
            return _CheckListGoodsContext.GetPagerPreviewList(checkListId, query);
        }
        public CheckList Create(CheckListCreateApiModel created, int departmentId, int userId)
        {
            return _CheckListContext.Create(created, departmentId, userId);
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
        public int Bill(int id)
        {
            return _CheckListContext.Bill(id);
        }
    }
}
