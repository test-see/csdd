using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("user_verification_code")]
    public class UserVerificationCode
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("verification_code")]
        public string VerificationCode { get; set; }
        [Column("expiration")]
        public DateTime Expiration { get; set; }
        [Column("createtime")]
        public DateTime CreateTime { get; set; }
        [Column("is_active")]
        public int IsActive { get; set; }
    }
}
