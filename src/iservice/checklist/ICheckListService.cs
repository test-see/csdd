using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.model;
using irespository.checklist.profile.model;

namespace iservice.checklist
{
    public interface ICheckListService
    {
        PagerResult<CheckListApiModel> GetPagerList(PagerQuery<CheckListQueryModel> query, int hospitalId);
        CheckListPreviewApiModel GetPagerPreviewList(int checkListId, PagerQuery<CheckListGoodsPreviewQueryModel> query);
        CheckList Create(CheckListCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, CheckListUpdateApiModel updated);
        CheckListIndexApiModel GetIndex(int id);
        int Submit(int id);
        int Bill(int id);
    }
}
