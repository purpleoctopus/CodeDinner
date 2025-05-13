using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBreakfast.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVisibleProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVerified",
                table: "Courses",
                newName: "IsVisible");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Module",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "IsVisible",
                table: "Courses",
                newName: "IsVerified");
        }
    }
}
