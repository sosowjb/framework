using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using SOSOWJB.Framework.Authorization.Users;
using SOSOWJB.Framework.KYP.Inventory;

namespace SOSOWJB.Framework.KYP.Auctions
{
    [Table("KYP_Bids")]
    public class Bid : Entity
    {
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public DateTime PublishedDateTime { get; set; }

        public int OrderIndex { get; set; }

        public double Price { get; set; }

        public int ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
    }
}