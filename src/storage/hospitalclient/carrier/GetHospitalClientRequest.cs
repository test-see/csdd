namespace nouns.client.profile
{
    public class GetHospitalClientRequest
    {
        public GetHospitalClientRequest(params int[] ids)
        {
            Ids = ids;
        }
        public int[] Ids { get; set; }
    }
}
