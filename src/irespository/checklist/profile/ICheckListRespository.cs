using foundation.config;
using foundation.ef5.poco;
using irespository.checklist.model;
using irespository.checklist.profile.enums;
using System.Threading.Tasks;

namespace irespository.checklist
{
    public interface ICheckListRespository
    {
        Task<PagerResult<CheckListApiModel>> GetPagerListAsync(PagerQuery<CheckListQueryModel> query, int hospitalId);
        CheckList Create(CheckListCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, CheckListUpdateApiModel updated);
        Task<CheckListIndexApiModel> GetIndexAsync(int id);
        int UpdateStatus(int id, CheckListStatus status);
    }
}
