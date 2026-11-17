using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatricConnect.Migrations
{
    /// <inheritdoc />
    public partial class AddReferenceComparisonRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComparisonRulesJson",
                table: "ProgrammeReferences",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComparisonRulesJson",
                table: "ProgrammeReferences");
        }
    }
}
