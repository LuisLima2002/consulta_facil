using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsultaFacil.Migrations
{
    /// <inheritdoc />
    public partial class updateSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "Schedules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "Schedules");
        }
    }
}
