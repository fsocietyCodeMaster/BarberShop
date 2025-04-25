using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BarberShop.Migrations
{
    /// <inheritdoc />
    public partial class fixingrelationship_barbershop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_BarberShops_AspNetUsers_T_User_ID",
                table: "T_BarberShops");

            migrationBuilder.DropIndex(
                name: "IX_T_BarberShops_T_User_ID",
                table: "T_BarberShops");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04bdd67f-9709-4402-9eb9-dee11c279636");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1aa22bf6-1210-4bee-9bf6-e44c0c2cee0b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81e1dce4-b5d0-4e06-b551-c3ab9e3d30cc");

            migrationBuilder.DropColumn(
                name: "T_User_ID",
                table: "T_BarberShops");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "T_BarberShops",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "T_BarberShop_ID",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_T_BarberShop_ID",
                table: "AspNetUsers",
                column: "T_BarberShop_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_T_BarberShops_T_BarberShop_ID",
                table: "AspNetUsers",
                column: "T_BarberShop_ID",
                principalTable: "T_BarberShops",
                principalColumn: "ID_Barbershop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_T_BarberShops_T_BarberShop_ID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_T_BarberShop_ID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "T_BarberShops");

            migrationBuilder.DropColumn(
                name: "T_BarberShop_ID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "T_User_ID",
                table: "T_BarberShops",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04bdd67f-9709-4402-9eb9-dee11c279636", null, "barbershop", "BARBERSHOP" },
                    { "1aa22bf6-1210-4bee-9bf6-e44c0c2cee0b", null, "barber", "BARBER" },
                    { "81e1dce4-b5d0-4e06-b551-c3ab9e3d30cc", null, "user", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_BarberShops_T_User_ID",
                table: "T_BarberShops",
                column: "T_User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_T_BarberShops_AspNetUsers_T_User_ID",
                table: "T_BarberShops",
                column: "T_User_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
