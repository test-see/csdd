namespace irespository.checklist.model
{
    public class CheckListGoodsCreateApiModel
    {
        public int CheckListId { get; set; }
        public int HospitalGoodsId { get; set; }
        public int CheckQty { get; set; }
        public int StoreQty { get; set; }
    }
}
