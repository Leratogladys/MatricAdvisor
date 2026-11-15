using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatricConnect.Migrations
{
    /// <inheritdoc />
    public partial class ExtendVerifiedIntakes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdmissionScoreLabel",
                table: "ProgrammeIntakes",
                type: "TEXT",
                nullable: false,
                defaultValue: "APS");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ApplicationClosingTime",
                table: "ProgrammeIntakes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RequiredEnglish",
                table: "ProgrammeIntakes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RequiredPhysicalScience",
                table: "ProgrammeIntakes",
                type: "TEXT",
                nullable: true);

            // Keep the published scoring label on Wits records already present in SQLite.
            migrationBuilder.Sql("""
                UPDATE ProgrammeIntakes
                SET AdmissionScoreLabel = 'Wits APS'
                WHERE OfficialProgrammeCode IN ('SBA13', 'SBA21')
                  AND ProgrammeId IN (
                    SELECT p.Id FROM Programmes p
                    JOIN Universities u ON u.Id = p.UniversityId
                    WHERE u.Name = 'University of the Witwatersrand'
                  );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmissionScoreLabel",
                table: "ProgrammeIntakes");

            migrationBuilder.DropColumn(
                name: "ApplicationClosingTime",
                table: "ProgrammeIntakes");

            migrationBuilder.DropColumn(
                name: "RequiredEnglish",
                table: "ProgrammeIntakes");

            migrationBuilder.DropColumn(
                name: "RequiredPhysicalScience",
                table: "ProgrammeIntakes");
        }
    }
}
