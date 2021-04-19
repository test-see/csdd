using domain.checklist;
using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.goods.model;
using irespository.checklist.model;
using iservice.checklist;
using System.Threading.Tasks;

namespace service.checklist
{
    public class CheckListGoodsService : ICheckListGoodsService
    {
        private readonly CheckListGoodsContext _CheckListGoodsContext;
        public CheckListGoodsService(CheckListGoodsContext CheckListGoodsContext)
        {
            _CheckListGoodsContext = CheckListGoodsContext;
        }
        public async Task<PagerResult<CheckListGoodsListApiModel>> GetPagerListAsync(PagerQuery<CheckListGoodsQueryModel> query)
        {
            return await _CheckListGoodsContext.GetPagerListAsync(query);
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
