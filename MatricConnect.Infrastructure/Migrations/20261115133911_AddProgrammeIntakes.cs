using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatricConnect.Migrations
{
    /// <inheritdoc />
    public partial class AddProgrammeIntakes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgrammeIntakes",
                columns: table => new
                {
                    ProgrammeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IntakeYear = table.Column<int>(type: "INTEGER", nullable: false),
                    OfficialProgrammeCode = table.Column<string>(type: "TEXT", nullable: false),
                    ApplicationDeadline = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    RequiredAps = table.Column<int>(type: "INTEGER", nullable: false),
                    RequiredMathematics = table.Column<decimal>(type: "TEXT", nullable: false),
                    AdditionalRequirements = table.Column<string>(type: "TEXT", nullable: false),
                    RequirementsSourceUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ApplicationDatesSourceUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ApsGuideUrl = table.Column<string>(type: "TEXT", nullable: false),
                    VerifiedOn = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeIntakes", x => new { x.ProgrammeId, x.IntakeYear });
                    table.ForeignKey(
                        name: "FK_ProgrammeIntakes_Programmes_ProgrammeId",
                        column: x => x.ProgrammeId,
                        principalTable: "Programmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgrammeIntakes");
        }
    }
}
