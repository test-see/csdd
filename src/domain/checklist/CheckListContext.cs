using domain.store;
using foundation.config;
using foundation.ef5;
using foundation.ef5.poco;
using irespository.checklist;
using irespository.checklist.model;
using irespository.checklist.profile.enums;

namespace domain.checklist
{
    public class CheckListContext
    {
        private readonly ICheckListRespository _CheckListRespository;
        private readonly StoreContext _storeContext;
        private readonly DefaultDbTransaction _defaultDbTransaction;
        public CheckListContext(ICheckListRespository CheckListRespository,
            StoreContext storeContext,
            DefaultDbTransaction defaultDbTransaction)
        {
            _CheckListRespository = CheckListRespository;
            _storeContext = storeContext;
            _defaultDbTransaction = defaultDbTransaction;
        }

        public PagerResult<CheckListApiModel> GetPagerList(PagerQuery<CheckListQueryModel> query, int hospitalId)
        {
            return _CheckListRespository.GetPagerList(query, hospitalId);
        }
        public CheckList Create(CheckListCreateApiModel created, int userId)
        {
            return _CheckListRespository.Create(created, userId);
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
        public int Bill(int id, int userId)
        {

            return _CheckListRespository.UpdateStatus(id, CheckListStatus.Billed);
        }
    }
}
