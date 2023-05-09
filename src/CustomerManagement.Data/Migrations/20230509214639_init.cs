using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalTown = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Middlenames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContactType = table.Column<int>(type: "int", nullable: false),
                    IsPreferred = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactDetails_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDefaultAddress = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresses", x => new { x.CustomerId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AddressLine1", "AddressLine2", "AddressLine3", "County", "PostCode", "PostalTown" },
                values: new object[,]
                {
                    { new Guid("38ed5f3f-3460-40ba-8009-93371fefdfa2"), "22 Oil Drum Lane", null, null, null, "W1A 1AA", "East Cheam" },
                    { new Guid("7242a930-fc47-4244-a97d-52b515aea1e4"), "22 Acacia Avenue", null, null, null, "M60 1AA", "Salford" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "FirstName", "Middlenames", "Surname", "Title" },
                values: new object[] { new Guid("e5eafda2-c5d9-46b5-b364-95bb6da27b58"), "Darcy", null, "Carter", "Mr" });

            migrationBuilder.InsertData(
                table: "ContactDetails",
                columns: new[] { "Id", "ContactType", "CustomerId", "Detail", "IsPreferred" },
                values: new object[,]
                {
                    { new Guid("de920a0d-9453-487b-9c65-4ee865bd417f"), 2, new Guid("e5eafda2-c5d9-46b5-b364-95bb6da27b58"), "0123456789", false },
                    { new Guid("f5eea8e5-accf-4593-bcd9-016a897cd0e1"), 0, new Guid("e5eafda2-c5d9-46b5-b364-95bb6da27b58"), "dcarter.customermanagement@gmail.com", true }
                });

            migrationBuilder.InsertData(
                table: "CustomerAddresses",
                columns: new[] { "AddressId", "CustomerId", "IsDefaultAddress" },
                values: new object[,]
                {
                    { new Guid("38ed5f3f-3460-40ba-8009-93371fefdfa2"), new Guid("e5eafda2-c5d9-46b5-b364-95bb6da27b58"), true },
                    { new Guid("7242a930-fc47-4244-a97d-52b515aea1e4"), new Guid("e5eafda2-c5d9-46b5-b364-95bb6da27b58"), false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PostalTown",
                table: "Addresses",
                column: "PostalTown");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PostCode",
                table: "Addresses",
                column: "PostCode");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_CustomerId",
                table: "ContactDetails",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_Detail",
                table: "ContactDetails",
                column: "Detail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_AddressId",
                table: "CustomerAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_FirstName_Surname",
                table: "Customers",
                columns: new[] { "FirstName", "Surname" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
