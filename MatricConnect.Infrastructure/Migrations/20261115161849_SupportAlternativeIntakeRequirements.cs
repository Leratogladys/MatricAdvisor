using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatricConnect.Migrations
{
    /// <inheritdoc />
    public partial class SupportAlternativeIntakeRequirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RequiredMathematics",
                table: "ProgrammeIntakes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "NextApplicationCycleNote",
                table: "ProgrammeIntakes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextApplicationCycleNote",
                table: "ProgrammeIntakes");

            migrationBuilder.AlterColumn<decimal>(
                name: "RequiredMathematics",
                table: "ProgrammeIntakes",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
