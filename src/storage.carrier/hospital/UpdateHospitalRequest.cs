﻿using Mediator.Net.Contracts;

namespace irespository.hospital.model
{
    public class UpdateHospitalRequest : IRequest
    {
        public string Name { get; set; }
        public int ConsumeDays { get; set; }
        public string Remark { get; set; }
        public int Id { get; set; }
    }
}
