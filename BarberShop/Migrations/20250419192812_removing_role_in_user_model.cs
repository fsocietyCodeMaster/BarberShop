using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BarberShop.Migrations
{
    /// <inheritdoc />
    public partial class removing_role_in_user_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "195372d1-628c-4f0e-b359-fe2d4b3b760e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4783c0c5-fbb0-44c6-b0cb-8886d00b84cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab440c98-5231-4c6f-b326-126fee679e14");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe1205be-f91a-41e9-b80a-584ac392e068");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04bdd67f-9709-4402-9eb9-dee11c279636", null, "barbershop", "BARBERSHOP" },
                    { "1aa22bf6-1210-4bee-9bf6-e44c0c2cee0b", null, "barber", "BARBER" },
                    { "81e1dce4-b5d0-4e06-b551-c3ab9e3d30cc", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "195372d1-628c-4f0e-b359-fe2d4b3b760e", null, "Admin", "ADMIN" },
                    { "4783c0c5-fbb0-44c6-b0cb-8886d00b84cd", null, "User", "USER" },
                    { "ab440c98-5231-4c6f-b326-126fee679e14", null, "BarberShop", "BARBERSHOP" },
                    { "fe1205be-f91a-41e9-b80a-584ac392e068", null, "Barber", "BARBER" }
                });
        }
    }
}
