using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace SOSOWJB.Framework.KYP.Addresses
{
    [Table("KYP_Districts")]
    public class District : Entity
    {
        public string Name { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}