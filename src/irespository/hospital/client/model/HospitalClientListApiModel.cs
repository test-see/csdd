﻿using irespository.hospital.profile.model;
using System;

namespace irespository.hospital.client.model
{
    public class HospitalClientListApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetHospitalResponse Hospital { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
    }
}
