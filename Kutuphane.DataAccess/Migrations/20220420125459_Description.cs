using Microsoft.EntityFrameworkCore.Migrations;

namespace Kutuphane.DataAccess.Migrations
{
    public partial class Description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookImage",
                table: "Books",
                newName: "YayinYili");

            migrationBuilder.AddColumn<string>(
                name: "BookDescription",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookDescription",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "YayinYili",
                table: "Books",
                newName: "BookImage");
        }
    }
}
