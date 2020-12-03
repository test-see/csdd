using foundation.ef5.poco;
using System.Collections.Generic;

namespace iservice.data.model
{
    public class DataTouristRegisterApiModel
    {
        public IEnumerable<DataIdentityCategory> IdentityCategories { get; set; }
        public IEnumerable<DataProvince> Provinces { get; set; }
    }
}