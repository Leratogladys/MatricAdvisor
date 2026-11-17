using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatricConnect.Migrations
{
    /// <inheritdoc />
    public partial class SaveProgrammeReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedProgrammeReferences",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProgrammeReferenceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedProgrammeReferences", x => new { x.StudentId, x.ProgrammeReferenceId });
                    table.ForeignKey(
                        name: "FK_SavedProgrammeReferences_ProgrammeReferences_ProgrammeReferenceId",
                        column: x => x.ProgrammeReferenceId,
                        principalTable: "ProgrammeReferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedProgrammeReferences_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedProgrammeReferences_ProgrammeReferenceId",
                table: "SavedProgrammeReferences",
                column: "ProgrammeReferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedProgrammeReferences");
        }
    }
}
