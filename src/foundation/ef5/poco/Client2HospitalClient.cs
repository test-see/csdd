using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("client_2hospital_client")]
    public class Client2HospitalClient
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("client_id")]
        public int ClientId { get; set; }
        [Column("hospital_client_id")]
        public int HospitalClientId { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("createuser_id")]
        public int CreateUserId { get; set; }
    }
}
