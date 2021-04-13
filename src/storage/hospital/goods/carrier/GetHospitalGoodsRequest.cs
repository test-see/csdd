namespace storage.hospitalgoods.carrier
{
    public class GetHospitalGoodsRequest
    {
        public GetHospitalGoodsRequest(params int[] ids)
        {
            Ids = ids;
        }
        public int[] Ids { get; set; }
    }
}
