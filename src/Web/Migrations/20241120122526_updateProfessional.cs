using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsultaFacil.Migrations
{
    /// <inheritdoc />
    public partial class updateProfessional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Profissionals",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "employmentDate",
                table: "Profissionals",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Profissionals");

            migrationBuilder.DropColumn(
                name: "employmentDate",
                table: "Profissionals");
        }
    }
}
