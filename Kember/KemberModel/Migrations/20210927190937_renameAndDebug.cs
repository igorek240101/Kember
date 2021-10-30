using Microsoft.EntityFrameworkCore.Migrations;

namespace KemberModel.Migrations
{
    public partial class renameAndDebug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_OwnerKey",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "OpenKey",
                table: "Users",
                newName: "SecurityKey");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OwnerKey",
                table: "Logs",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Logs",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_OwnerKey",
                table: "Logs",
                newName: "IX_Logs_OwnerId");

            migrationBuilder.AddColumn<string>(
                name: "Metric",
                table: "Logs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_OwnerId",
                table: "Logs",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_OwnerId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Metric",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "SecurityKey",
                table: "Users",
                newName: "OpenKey");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Logs",
                newName: "OwnerKey");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Logs",
                newName: "Key");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_OwnerId",
                table: "Logs",
                newName: "IX_Logs_OwnerKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_OwnerKey",
                table: "Logs",
                column: "OwnerKey",
                principalTable: "Users",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
