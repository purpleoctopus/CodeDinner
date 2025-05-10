using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBreakfast.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdditionJsonToUserActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalJson",
                table: "UserActivities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalJson",
                table: "UserActivities");
        }
    }
}
