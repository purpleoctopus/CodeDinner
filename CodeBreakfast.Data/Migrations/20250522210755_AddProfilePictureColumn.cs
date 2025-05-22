using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBreakfast.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePictureColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserConfigs_UserId",
                table: "UserConfigs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserConfigs_AspNetUsers_UserId",
                table: "UserConfigs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserConfigs_AspNetUsers_UserId",
                table: "UserConfigs");

            migrationBuilder.DropIndex(
                name: "IX_UserConfigs_UserId",
                table: "UserConfigs");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");
        }
    }
}
