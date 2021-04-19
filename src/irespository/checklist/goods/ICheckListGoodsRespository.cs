using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.goods.model;
using irespository.checklist.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace irespository.checklist
{
    public interface ICheckListGoodsRespository
    {
        Task<PagerResult<CheckListGoodsListApiModel>> GetPagerListAsync(PagerQuery<CheckListGoodsQueryModel> query);
        Task<PagerResult<CheckListGoodsPreviewListApiModel>> GetPagerPreviewListAsync(int checkListId, PagerQuery<CheckListGoodsPreviewQueryModel> query);
        decimal GetPreviewListAmount(int checkListId);
        CheckListGoods Create(CheckListGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, CheckListGoodsUpdateApiModel updated);
        Task<IList<CheckListGoodsPreviewListApiModel>> GetPreviewListAsync(int checkListId);
    }
}
