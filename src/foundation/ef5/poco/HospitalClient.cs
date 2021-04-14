using Mediator.Net.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("hospital_client")]
    public class HospitalClient:IResponse
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("hospital_id")]
        public int HospitalId { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
    }
}
