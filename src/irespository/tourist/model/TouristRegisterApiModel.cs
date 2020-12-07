using System.Collections.Generic;

namespace irespository.tourist.model
{
    public class TouristRegisterApiModel
    {
        public TouristRegisterProfileApiModel Profile { get; set; }
        public IEnumerable<TouristRegisterClientPreferenceApiModel> ClientPreferences { get; set; }
        public IEnumerable<TouristRegisterDepartmentPreferenceApiModel> HospitalPreferences { get; set; }
        public IEnumerable<TouristRegisterSalesPreferenceApiModel> SalesPreferences { get; set; }
    }
}
