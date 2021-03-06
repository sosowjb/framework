﻿using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOSOWJB.Framework.Authorization.Roles;
using SOSOWJB.Framework.Authorization.Users;
using SOSOWJB.Framework.Chat;
using SOSOWJB.Framework.Editions;
using SOSOWJB.Framework.Friendships;
using SOSOWJB.Framework.KYP.Accounts;
using SOSOWJB.Framework.KYP.Addresses;
using SOSOWJB.Framework.KYP.Auctions;
using SOSOWJB.Framework.KYP.Inventory;
using SOSOWJB.Framework.KYP.Orders;
using SOSOWJB.Framework.MultiTenancy;
using SOSOWJB.Framework.MultiTenancy.Accounting;
using SOSOWJB.Framework.MultiTenancy.Payments;
using SOSOWJB.Framework.Storage;

namespace SOSOWJB.Framework.EntityFrameworkCore
{
    public class FrameworkDbContext : AbpZeroDbContext<Tenant, Role, User, FrameworkDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        // 快易拍 **************************************************

        public virtual DbSet<KypAccount> KypAccounts { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Province> Provinces { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Bid> Bids { get; set; }

        public virtual DbSet<Item> Items { get; set; }

        public virtual DbSet<ItemPic> ItemPics { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        // ***********************************************************

        public FrameworkDbContext(DbContextOptions<FrameworkDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { e.PaymentId, e.Gateway });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
