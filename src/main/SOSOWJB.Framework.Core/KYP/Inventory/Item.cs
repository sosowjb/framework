using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using SOSOWJB.Framework.KYP.Auctions;

namespace SOSOWJB.Framework.KYP.Inventory
{
    [Table("KYP_Items")]
    public class Item : FullAuditedEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 是否已卖出
        /// </summary>
        public bool IsSoldOut { get; set; }

        /// <summary>
        ///  原价
        /// </summary>
        public double OriginalPrice { get; set; }

        /// <summary>
        /// 起拍价
        /// </summary>
        public double PriceAnnounced { get; set; }

        /// <summary>
        /// 加价
        /// </summary>
        public double AddingStepPrice { get; set; }

        public DateTime PublishedDateTime { get; set; }

        public DateTime Deadline { get; set; }

        public int FirstBidId { get; set; }

        [ForeignKey("FirstBidId")]
        public Bid FirstBid { get; set; }
    }
}