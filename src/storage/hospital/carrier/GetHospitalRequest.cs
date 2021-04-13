namespace nouns.client.profile
{
    public class GetHospitalRequest
    {
        public GetHospitalRequest(params int[] ids)
        {
            Ids = ids;
        }
        public int[] Ids { get; set; }
    }
}
