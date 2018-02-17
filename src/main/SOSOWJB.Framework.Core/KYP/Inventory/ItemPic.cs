using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace SOSOWJB.Framework.KYP.Inventory
{
    [Table("KYP_ItemPics")]
    public class ItemPic : FullAuditedEntity
    {
        public int OrderIndex { get; set; }

        public string Summary { get; set; }

        public string Path { get; set; }

        public int ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
    }
}