using foundation.config;
using foundation.ef5.poco;
using irespository.checklist;
using irespository.checklist.goods.model;
using irespository.checklist.model;
using System.Threading.Tasks;

namespace domain.checklist
{
    public class CheckListGoodsContext
    {
        private readonly ICheckListGoodsRespository _CheckListGoodsRespository;
        public CheckListGoodsContext(ICheckListGoodsRespository CheckListGoodsRespositoryy)
        {
            _CheckListGoodsRespository = CheckListGoodsRespositoryy;
        }

        public async Task<PagerResult<CheckListGoodsListApiModel>> GetPagerListAsync(PagerQuery<CheckListGoodsQueryModel> query)
        {
            return await _CheckListGoodsRespository.GetPagerListAsync(query);
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
        public async Task<PagerResult<CheckListGoodsPreviewListApiModel>> GetPagerPreviewListAsync(int checkListId, PagerQuery<CheckListGoodsPreviewQueryModel> query)
        {
            return await _CheckListGoodsRespository.GetPagerPreviewListAsync(checkListId, query);
        }
        public decimal GetPreviewListAmount(int checkListId)
        {
            return _CheckListGoodsRespository.GetPreviewListAmount(checkListId);
        }
    }
}
