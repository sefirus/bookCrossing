using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddNullabuilityToFKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WriterId",
                table: "Pictures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ShelfId",
                table: "Pictures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Pictures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Pictures",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "WriterId",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShelfId",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
