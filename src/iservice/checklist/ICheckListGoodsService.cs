using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.goods.model;
using irespository.checklist.model;
using System.Threading.Tasks;

namespace iservice.checklist
{
    public interface ICheckListGoodsService
    {
        Task<PagerResult<CheckListGoodsListApiModel>> GetPagerListAsync(PagerQuery<CheckListGoodsQueryModel> query);
        CheckListGoods Create(CheckListGoodsCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, CheckListGoodsUpdateApiModel updated);
    }
}
