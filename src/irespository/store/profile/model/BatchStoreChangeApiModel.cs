using System.Collections.Generic;

namespace irespository.store.model
{
    public class BatchStoreChangeApiModel
    {
        public IList<KeyValuePair<int, int>> HospitalGoods { get; set; }
        public int ChangeTypeId { get; set; }
    }
}
