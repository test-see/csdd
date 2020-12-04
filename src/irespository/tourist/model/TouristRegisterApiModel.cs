using foundation.ef5.poco;
using System.Collections.Generic;

namespace irespository.tourist.model
{
    public class TouristRegisterApiModel
    {
        public TouristRegisterStepOneApiModel Profile { get; set; }
        public IEnumerable<TouristClientPreference> TouristClientPreferences { get; set; }
        public IEnumerable<TouristHospitalPreference> TouristHospitalPreferences { get; set; }
        public IEnumerable<TouristSalesPreference> TouristSalesPreferences { get; set; }
    }
}
