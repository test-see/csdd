using foundation.config;
using foundation.ef5.poco;
using irespository.checklist;
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

        public PagerResult<CheckListGoodsApiModel> GetPagerList(PagerQuery<CheckListGoodsQueryModel> query)
        {
            return _CheckListGoodsRespository.GetPagerList(query);
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
