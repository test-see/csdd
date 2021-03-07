using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.model;
using irespository.checklist.profile.enums;

namespace irespository.checklist
{
    public interface ICheckListRespository
    {
        PagerResult<CheckListApiModel> GetPagerList(PagerQuery<CheckListQueryModel> query, int hospitalId);
        CheckList Create(CheckListCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, CheckListUpdateApiModel updated);
        CheckListIndexApiModel GetIndex(int id);
        int UpdateStatus(int id, CheckListStatus status);
    }
}
