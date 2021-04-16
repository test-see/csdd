namespace storage.hospitalgoods.carrier
{
    public class GetHospitalGoodsByBarcodeRequest
    {
        public GetHospitalGoodsByBarcodeRequest(string barcode)
        {
            Barcode = barcode;
        }
        public string Barcode { get; set; }
    }
}
