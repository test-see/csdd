﻿using System;

namespace irespository.hospital.model
{
    public class HospitalGoodsListApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HospitalName { get; set; }
        public string Spec { get; set; }
        public string UnitPurchase { get; set; }
        public string Producer { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public int IsActive { get; set; }
    }
}