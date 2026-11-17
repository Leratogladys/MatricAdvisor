using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MatricConnect.Data;

#nullable disable

namespace MatricConnect.Migrations
{
    /// <inheritdoc />
    public partial class AddProgrammeFacultiesAndReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Faculty",
                table: "Programmes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.Sql(ReviewedFacultyAssignments.Sql);

            migrationBuilder.CreateTable(
                name: "ProgrammeReferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UniversityId = table.Column<int>(type: "INTEGER", nullable: true),
                    TvetCollegeId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Group = table.Column<string>(type: "TEXT", nullable: false),
                    Campuses = table.Column<string>(type: "TEXT", nullable: true),
                    EntryRequirements = table.Column<string>(type: "TEXT", nullable: true),
                    IntakeYear = table.Column<int>(type: "INTEGER", nullable: true),
                    Availability = table.Column<string>(type: "TEXT", nullable: false),
                    SourceUrl = table.Column<string>(type: "TEXT", nullable: false),
                    VerifiedOn = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeReferences", x => x.Id);
                    table.CheckConstraint("CK_ProgrammeReference_OneInstitution", "(UniversityId IS NULL) <> (TvetCollegeId IS NULL)");
                    table.ForeignKey(
                        name: "FK_ProgrammeReferences_TvetColleges_TvetCollegeId",
                        column: x => x.TvetCollegeId,
                        principalTable: "TvetColleges",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgrammeReferences_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeReferences_TvetCollegeId_Group_Name",
                table: "ProgrammeReferences",
                columns: new[] { "TvetCollegeId", "Group", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeReferences_UniversityId_Group_Name",
                table: "ProgrammeReferences",
                columns: new[] { "UniversityId", "Group", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgrammeReferences");

            migrationBuilder.DropColumn(
                name: "Faculty",
                table: "Programmes");
        }
    }
}
