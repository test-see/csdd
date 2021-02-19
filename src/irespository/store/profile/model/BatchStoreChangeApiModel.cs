using irespository.store.profile.model;
using System.Collections.Generic;

namespace irespository.store.model
{
    public class BatchStoreChangeApiModel
    {
        public int ChangeTypeId { get; set; }
        public IList<StoreChangeGoodsValueModel> HospitalChangeGoods { get; set; }
    }
}
