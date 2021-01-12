﻿using foundation.config;
using foundation.ef5.poco;
using irespository.hospital.department.model;

namespace irespository.hospital
{
    public interface IHospitalDepartmentRespository
    {
        PagerResult<HospitalDepartmentListApiModel> GetPagerList(PagerQuery<HospitalDepartmentListQueryModel> query);
        HospitalDepartment Create(HospitalDepartmentCreateApiModel created, int userId);
        int Delete(int id);
        int Update(int id, HospitalDepartmentUpdateApiModel updated);
    }
}