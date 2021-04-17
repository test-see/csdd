using Mediator.Net.Contracts;

namespace storage.hospitalgoods.carrier
{
    public class GetHospitalGoodsByBarcodeRequest:IRequest
    {
        public GetHospitalGoodsByBarcodeRequest(string barcode)
        {
            Barcode = barcode;
        }
        public string Barcode { get; set; }
    }
}
