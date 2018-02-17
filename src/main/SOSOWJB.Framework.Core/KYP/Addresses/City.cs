using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace SOSOWJB.Framework.KYP.Addresses
{
    [Table("KYP_Cities")]
    public class City : Entity
    {
        public string Name { get; set; }

        public int ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }
    }
}