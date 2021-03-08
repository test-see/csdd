using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.goods.model;
using irespository.checklist.model;
using System.Collections.Generic;

namespace irespository.checklist
{
    public interface ICheckListGoodsRespository
    {
        PagerResult<CheckListGoodsListApiModel> GetPagerList(PagerQuery<CheckListGoodsQueryModel> query);
        PagerResult<CheckListGoodsPreviewListApiModel> GetPagerPreviewList(int checkListId, PagerQuery<CheckListGoodsPreviewQueryModel> query);
        decimal GetPreviewListAmount(int checkListId);
        CheckListGoods Create(CheckListGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, CheckListGoodsUpdateApiModel updated);
        IList<CheckListGoodsPreviewListApiModel> GetPreviewList(int checkListId);
    }
}
