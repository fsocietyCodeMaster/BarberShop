using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Migrations
{
    /// <inheritdoc />
    public partial class datetime_for_appointment_barberId_insteadof_userid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Appointments_AspNetUsers_T_User_ID",
                table: "T_Appointments");

            migrationBuilder.RenameColumn(
                name: "T_User_ID",
                table: "T_Appointments",
                newName: "T_Barber_ID");

            migrationBuilder.RenameIndex(
                name: "IX_T_Appointments_T_User_ID",
                table: "T_Appointments",
                newName: "IX_T_Appointments_T_Barber_ID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "T_Appointments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Appointments_AspNetUsers_T_Barber_ID",
                table: "T_Appointments",
                column: "T_Barber_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Appointments_AspNetUsers_T_Barber_ID",
                table: "T_Appointments");

            migrationBuilder.RenameColumn(
                name: "T_Barber_ID",
                table: "T_Appointments",
                newName: "T_User_ID");

            migrationBuilder.RenameIndex(
                name: "IX_T_Appointments_T_Barber_ID",
                table: "T_Appointments",
                newName: "IX_T_Appointments_T_User_ID");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "AppointmentDate",
                table: "T_Appointments",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Appointments_AspNetUsers_T_User_ID",
                table: "T_Appointments",
                column: "T_User_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
