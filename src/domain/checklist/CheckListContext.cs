using foundation.config;
using foundation.ef5.poco;
using irespository.checklist;
using irespository.checklist.model;
using irespository.checklist.profile.enums;

namespace domain.checklist
{
    public class CheckListContext
    {
        private readonly ICheckListRespository _CheckListRespository;
        public CheckListContext(ICheckListRespository CheckListRespository)
        {
            _CheckListRespository = CheckListRespository;
        }

        public PagerResult<CheckListApiModel> GetPagerList(PagerQuery<CheckListQueryModel> query)
        {
            return _CheckListRespository.GetPagerList(query);
        }
        public CheckList Create(CheckListCreateApiModel created, int departmentId, int userId)
        {
            return _CheckListRespository.Create(created, departmentId, userId);
        }
        public int Delete(int id)
        {
            return _CheckListRespository.Delete(id);
        }
        public int Update(int id, CheckListUpdateApiModel updated)
        {
            return _CheckListRespository.Update(id, updated);
        }
        public CheckListIndexApiModel GetIndex(int id)
        {
            var goods = _CheckListRespository.GetIndex(id);
            return goods;
        }
        public int Submit(int id)
        {
            return _CheckListRespository.UpdateStatus(id, CheckListStatus.Submited);
        }
        public int Bill(int id)
        {
            return _CheckListRespository.UpdateStatus(id, CheckListStatus.Billed);
        }
    }
}
