﻿using foundation.ef5.poco;
using irespository.hospital.department.model;
using System;

namespace irespository.invoice.model
{
    public class InvoiceListApiModel
    {
        public int Id { get; set; }
        public HospitalDepartmentValueModel HospitalDepartment { get; set; }
        public DataInvoiceType InvoiceType { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
