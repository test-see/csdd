using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.model;

namespace irespository.checklist
{
    public interface ICheckListGoodsRespository
    {
        PagerResult<CheckListGoodsApiModel> GetPagerList(PagerQuery<CheckListGoodsQueryModel> query);
        CheckListGoods Create(CheckListGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, CheckListGoodsUpdateApiModel updated);
    }
}
