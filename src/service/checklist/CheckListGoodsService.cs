using domain.checklist;
using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.goods.model;
using irespository.checklist.model;
using iservice.checklist;

namespace service.checklist
{
    public class CheckListGoodsService : ICheckListGoodsService
    {
        private readonly CheckListGoodsContext _CheckListGoodsContext;
        public CheckListGoodsService(CheckListGoodsContext CheckListGoodsContext)
        {
            _CheckListGoodsContext = CheckListGoodsContext;
        }
        public PagerResult<CheckListGoodsListApiModel> GetPagerList(PagerQuery<CheckListGoodsQueryModel> query)
        {
            return _CheckListGoodsContext.GetPagerList(query);
        }
        public CheckListGoods Create(CheckListGoodsCreateApiModel created, int userId)
        {
            return _CheckListGoodsContext.Create(created, userId);
        }

        public int Delete(int id)
        {
            return _CheckListGoodsContext.Delete(id);
        }

        public int Update(int id, CheckListGoodsUpdateApiModel updated)
        {
            return _CheckListGoodsContext.Update(id, updated);
        }
    }
}
