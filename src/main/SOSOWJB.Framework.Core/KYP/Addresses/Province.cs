using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace SOSOWJB.Framework.KYP.Addresses
{
    [Table("KYP_Provinces")]
    public class Province : Entity
    {
        public string Name { get; set; }
    }
}