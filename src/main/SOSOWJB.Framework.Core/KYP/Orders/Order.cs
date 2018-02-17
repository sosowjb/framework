using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using SOSOWJB.Framework.Authorization.Users;
using SOSOWJB.Framework.KYP.Addresses;
using SOSOWJB.Framework.KYP.Inventory;

namespace SOSOWJB.Framework.KYP.Orders
{
    [Table("KYP_Orders")]
    public class Order : FullAuditedEntity
    {
        public string OrderCode { get; set; }

        public Item Item { get; set; }

        public double Amount { get; set; }

        public DateTime OrderDateTime { get; set; }

        public OrderStatus Status { get; set; }

        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public long BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        public User Buyer { get; set; }

        public long SellerId { get; set; }

        [ForeignKey("SellerId")]
        public User Seller { get; set; }
    }
}