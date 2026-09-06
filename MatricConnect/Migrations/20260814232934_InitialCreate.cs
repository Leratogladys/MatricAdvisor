using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatricConnect.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    APSScore = table.Column<int>(type: "INTEGER", nullable: false),
                    MathematicsMark = table.Column<decimal>(type: "TEXT", nullable: false),
                    PhysicalScienceMark = table.Column<decimal>(type: "TEXT", nullable: false),
                    Province = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Province = table.Column<string>(type: "TEXT", nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    FieldOfStudy = table.Column<string>(type: "TEXT", nullable: false),
                    RequiredAPS = table.Column<int>(type: "INTEGER", nullable: false),
                    RequiredMathematics = table.Column<decimal>(type: "TEXT", nullable: false),
                    RequiredPhysicalScience = table.Column<decimal>(type: "TEXT", nullable: false),
                    ApplicationDeadline = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UniversityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programmes_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedProgrammes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProgrammeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedProgrammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedProgrammes_Programmes_ProgrammeId",
                        column: x => x.ProgrammeId,
                        principalTable: "Programmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedProgrammes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programmes_UniversityId",
                table: "Programmes",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedProgrammes_ProgrammeId",
                table: "SavedProgrammes",
                column: "ProgrammeId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedProgrammes_StudentId",
                table: "SavedProgrammes",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedProgrammes");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
