using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Migrations
{
    /// <inheritdoc />
    public partial class fixing_barberid_in_schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_BarberWorkSchedules_AspNetUsers_BarberId",
                table: "T_BarberWorkSchedules");

            migrationBuilder.DropIndex(
                name: "IX_T_BarberWorkSchedules_BarberId",
                table: "T_BarberWorkSchedules");

            migrationBuilder.DropColumn(
                name: "BarberId",
                table: "T_BarberWorkSchedules");

            migrationBuilder.AlterColumn<string>(
                name: "T_Barber_ID",
                table: "T_BarberWorkSchedules",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_T_BarberWorkSchedules_T_Barber_ID",
                table: "T_BarberWorkSchedules",
                column: "T_Barber_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_T_BarberWorkSchedules_AspNetUsers_T_Barber_ID",
                table: "T_BarberWorkSchedules",
                column: "T_Barber_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_BarberWorkSchedules_AspNetUsers_T_Barber_ID",
                table: "T_BarberWorkSchedules");

            migrationBuilder.DropIndex(
                name: "IX_T_BarberWorkSchedules_T_Barber_ID",
                table: "T_BarberWorkSchedules");

            migrationBuilder.AlterColumn<string>(
                name: "T_Barber_ID",
                table: "T_BarberWorkSchedules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "BarberId",
                table: "T_BarberWorkSchedules",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_BarberWorkSchedules_BarberId",
                table: "T_BarberWorkSchedules",
                column: "BarberId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_BarberWorkSchedules_AspNetUsers_BarberId",
                table: "T_BarberWorkSchedules",
                column: "BarberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
