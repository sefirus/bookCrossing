using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class RemovedPublisherPictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Publishers_PublisherId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_PublisherId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Pictures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_PublisherId",
                table: "Pictures",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Publishers_PublisherId",
                table: "Pictures",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
