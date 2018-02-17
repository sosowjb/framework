using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SOSOWJB.Framework.Migrations
{
    public partial class AddKyp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KYP_Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Balance = table.Column<double>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYP_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYP_Accounts_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KYP_Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYP_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KYP_Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYP_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYP_Cities_KYP_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "KYP_Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KYP_Districts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYP_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYP_Districts_KYP_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "KYP_Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KYP_Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DistrictId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYP_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYP_Addresses_KYP_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "KYP_Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KYP_Addresses_KYP_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "KYP_Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KYP_Addresses_KYP_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "KYP_Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KYP_Addresses_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KYP_Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    BuyerId = table.Column<long>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemId = table.Column<int>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    OrderCode = table.Column<string>(nullable: true),
                    OrderDateTime = table.Column<DateTime>(nullable: false),
                    SellerId = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYP_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYP_Orders_KYP_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "KYP_Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KYP_Orders_AbpUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KYP_Orders_AbpUsers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KYP_Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddingStepPrice = table.Column<double>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Deadline = table.Column<DateTime>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FirstBidId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSoldOut = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OriginalPrice = table.Column<double>(nullable: false),
                    PriceAnnounced = table.Column<double>(nullable: false),
                    PublishedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYP_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KYP_Bids",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ItemId = table.Column<int>(nullable: false),
                    OrderIndex = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    PublishedDateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYP_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYP_Bids_KYP_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "KYP_Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KYP_Bids_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KYP_ItemPics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    OrderIndex = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYP_ItemPics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYP_ItemPics_KYP_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "KYP_Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Accounts_UserId",
                table: "KYP_Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Addresses_CityId",
                table: "KYP_Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Addresses_DistrictId",
                table: "KYP_Addresses",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Addresses_ProvinceId",
                table: "KYP_Addresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Addresses_UserId",
                table: "KYP_Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Bids_ItemId",
                table: "KYP_Bids",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Bids_UserId",
                table: "KYP_Bids",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Cities_ProvinceId",
                table: "KYP_Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Districts_CityId",
                table: "KYP_Districts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_ItemPics_ItemId",
                table: "KYP_ItemPics",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Items_FirstBidId",
                table: "KYP_Items",
                column: "FirstBidId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Orders_AddressId",
                table: "KYP_Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Orders_BuyerId",
                table: "KYP_Orders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Orders_ItemId",
                table: "KYP_Orders",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_KYP_Orders_SellerId",
                table: "KYP_Orders",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_KYP_Orders_KYP_Items_ItemId",
                table: "KYP_Orders",
                column: "ItemId",
                principalTable: "KYP_Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KYP_Items_KYP_Bids_FirstBidId",
                table: "KYP_Items",
                column: "FirstBidId",
                principalTable: "KYP_Bids",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KYP_Bids_KYP_Items_ItemId",
                table: "KYP_Bids");

            migrationBuilder.DropTable(
                name: "KYP_Accounts");

            migrationBuilder.DropTable(
                name: "KYP_ItemPics");

            migrationBuilder.DropTable(
                name: "KYP_Orders");

            migrationBuilder.DropTable(
                name: "KYP_Addresses");

            migrationBuilder.DropTable(
                name: "KYP_Districts");

            migrationBuilder.DropTable(
                name: "KYP_Cities");

            migrationBuilder.DropTable(
                name: "KYP_Provinces");

            migrationBuilder.DropTable(
                name: "KYP_Items");

            migrationBuilder.DropTable(
                name: "KYP_Bids");
        }
    }
}
