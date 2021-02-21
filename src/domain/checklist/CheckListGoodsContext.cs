using foundation.config;
using foundation.ef5.poco;
using irespository.checklist;
using irespository.checklist.goods.model;
using irespository.checklist.model;

namespace domain.checklist
{
    public class CheckListGoodsContext
    {
        private readonly ICheckListGoodsRespository _CheckListGoodsRespository;
        public CheckListGoodsContext(ICheckListGoodsRespository CheckListGoodsRespositoryy)
        {
            _CheckListGoodsRespository = CheckListGoodsRespositoryy;
        }

        public PagerResult<CheckListGoodsListApiModel> GetPagerList(PagerQuery<CheckListGoodsQueryModel> query)
        {
            return _CheckListGoodsRespository.GetPagerList(query);
        }
        public PagerResult<CheckListGoodsPreviewListApiModel> GetPagerPreviewList(int checkListId, PagerQuery<CheckListGoodsPreviewQueryModel> query)
        {
            return _CheckListGoodsRespository.GetPagerPreviewList(checkListId, query);
        }
        public CheckListGoods Create(CheckListGoodsCreateApiModel created, int userId)
        {
            return _CheckListGoodsRespository.Create(created, userId);
        }
        public int Delete(int id)
        {
            return _CheckListGoodsRespository.Delete(id);
        }
        public int Update(int id, CheckListGoodsUpdateApiModel updated)
        {
            return _CheckListGoodsRespository.Update(id, updated);
        }
    }
}
