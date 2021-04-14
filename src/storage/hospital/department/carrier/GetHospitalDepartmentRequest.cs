namespace storage.hospital.department.carrier
{
    public class GetHospitalDepartmentRequest
    {
        public GetHospitalDepartmentRequest(params int[] ids)
        {
            Ids = ids;
        }
        public int[] Ids { get; set; }
    }
}
