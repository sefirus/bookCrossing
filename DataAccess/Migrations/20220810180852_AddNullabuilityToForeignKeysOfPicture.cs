using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddNullabuilityToForeignKeysOfPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Writers_WriterId",
                table: "Pictures");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Writers_WriterId",
                table: "Pictures",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Writers_WriterId",
                table: "Pictures");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Writers_WriterId",
                table: "Pictures",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
