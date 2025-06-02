using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Migrations
{
    /// <inheritdoc />
    public partial class barber_schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "T_BarberWorkSchedules",
                columns: table => new
                {
                    ID_BarberWorkSchedule = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTimeMorning = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartTimeEvening = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTimeMorning = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTimeEvening = table.Column<TimeSpan>(type: "time", nullable: false),
                    ScopeTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    SaturdayWork = table.Column<bool>(type: "bit", nullable: false),
                    SundayWork = table.Column<bool>(type: "bit", nullable: false),
                    MondayWork = table.Column<bool>(type: "bit", nullable: false),
                    TuesdayWork = table.Column<bool>(type: "bit", nullable: false),
                    WednesdayWork = table.Column<bool>(type: "bit", nullable: false),
                    ThursdayWork = table.Column<bool>(type: "bit", nullable: false),
                    FridayWork = table.Column<bool>(type: "bit", nullable: false),
                    T_Barber_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarberId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BarberWorkSchedules", x => x.ID_BarberWorkSchedule);
                    table.ForeignKey(
                        name: "FK_T_BarberWorkSchedules_AspNetUsers_BarberId",
                        column: x => x.BarberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_BarberWorkSchedules_BarberId",
                table: "T_BarberWorkSchedules",
                column: "BarberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_BarberWorkSchedules");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "AspNetUsers",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "AspNetUsers",
                type: "time",
                nullable: true);
        }
    }
}
