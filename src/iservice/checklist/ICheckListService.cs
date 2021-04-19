using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.model;
using irespository.checklist.profile.model;
using System.Threading.Tasks;

namespace iservice.checklist
{
    public interface ICheckListService
    {
        Task<PagerResult<CheckListApiModel>> GetPagerListAsync(PagerQuery<CheckListQueryModel> query, int hospitalId);
        Task<CheckListPreviewApiModel> GetPagerPreviewListAsync(int checkListId, PagerQuery<CheckListGoodsPreviewQueryModel> query);
        Task<CheckList> CreateAsync(CheckListCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, CheckListUpdateApiModel updated);
        Task<CheckListIndexApiModel> GetIndexAsync(int id);
        int Submit(int id);
        Task<int> BillAsync(int id, int userId);
    }
}
