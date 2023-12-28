using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceWebApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionForCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Categories",
                newName: "ImageURL");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Categories",
                newName: "ImageUrl");
        }
    }
}
