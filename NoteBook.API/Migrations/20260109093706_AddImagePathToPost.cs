using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteBook.API.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Posts",
                newName: "ImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Posts",
                newName: "ImageUrl");
        }
    }
}
