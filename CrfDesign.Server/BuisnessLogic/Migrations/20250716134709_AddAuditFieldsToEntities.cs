using Microsoft.EntityFrameworkCore.Migrations;

namespace BuisnessLogic.Migrations
{
    public partial class AddAuditFieldsToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLockedForChanges",
                table: "QuestionTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatorUserId",
                table: "QuestionTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatorUserId",
                table: "CrfPages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLockedForChanges",
                table: "CrfPageComponents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatorUserId",
                table: "CrfPageComponents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLockedForChanges",
                table: "CrfOptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatorUserId",
                table: "CrfOptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLockedForChanges",
                table: "CrfOptionCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatorUserId",
                table: "CrfOptionCategories",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLockedForChanges",
                table: "QuestionTypes");

            migrationBuilder.DropColumn(
                name: "LastUpdatorUserId",
                table: "QuestionTypes");

            migrationBuilder.DropColumn(
                name: "LastUpdatorUserId",
                table: "CrfPages");

            migrationBuilder.DropColumn(
                name: "IsLockedForChanges",
                table: "CrfPageComponents");

            migrationBuilder.DropColumn(
                name: "LastUpdatorUserId",
                table: "CrfPageComponents");

            migrationBuilder.DropColumn(
                name: "IsLockedForChanges",
                table: "CrfOptions");

            migrationBuilder.DropColumn(
                name: "LastUpdatorUserId",
                table: "CrfOptions");

            migrationBuilder.DropColumn(
                name: "IsLockedForChanges",
                table: "CrfOptionCategories");

            migrationBuilder.DropColumn(
                name: "LastUpdatorUserId",
                table: "CrfOptionCategories");
        }
    }
}
