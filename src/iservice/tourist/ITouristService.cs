﻿using foundation.ef5.poco;
using System.Collections.Generic;

namespace iservice.tourist
{
    public interface ITouristService
    {
        IEnumerable<Hospital> GetHospitals(int provinceId);
        IEnumerable<HospitalDepartment> GetHospitalDepartments(int hospitalId);
    }
}
