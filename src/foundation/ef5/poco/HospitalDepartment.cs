using foundation.config;
using Mediator.Net.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace foundation.ef5.poco
{
    [Table("hospital_department")]
    public class HospitalDepartment:IResponse
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
        [Column("department_type_id")]
        public int DepartmentTypeId { get; set; }
        [Column("parent_id")]
        public int ParentId { get; set; }
        [Column("is_purchase_check")]
        public int IsPurchaseCheck { get; set; }

    }
}
