using Microsoft.EntityFrameworkCore.Migrations;

namespace Kutuphane.DataAccess.Migrations
{
    public partial class removeauthoroperations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operationss_Authors_AuthorID",
                table: "Operationss");

            migrationBuilder.DropIndex(
                name: "IX_Operationss_AuthorID",
                table: "Operationss");

            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "Operationss");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorID",
                table: "Operationss",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operationss_AuthorID",
                table: "Operationss",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Operationss_Authors_AuthorID",
                table: "Operationss",
                column: "AuthorID",
                principalTable: "Authors",
                principalColumn: "AuthorID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
