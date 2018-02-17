using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using SOSOWJB.Framework.Authorization.Users;

namespace SOSOWJB.Framework.KYP.Addresses
{
    [Table("KYP_Addresses")]
    public class Address : FullAuditedEntity
    {
        public int ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public District District { get; set; }

        public string Street { get; set; }

        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}