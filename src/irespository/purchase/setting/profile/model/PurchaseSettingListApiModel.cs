﻿using irespository.hospital.department.model;
using System;

namespace irespository.purchase.model
{
    public class PurchaseSettingListApiModel
    {
        public int Id { get; set; }
        public HospitalDepartmentValueModel HospitalDepartment { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
