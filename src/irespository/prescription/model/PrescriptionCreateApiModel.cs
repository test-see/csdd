using System.Collections.Generic;

namespace irespository.prescription.model
{
    public class PrescriptionCreateApiModel
    {
        public string Cardno { get; set; }
        public IList<KeyValuePair<int, int>> HospitalGoods { get; set; }
}
}
