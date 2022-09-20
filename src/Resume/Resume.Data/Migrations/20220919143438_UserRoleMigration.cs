using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resume.Data.Migrations
{
    public partial class UserRoleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Attachments_AttachmentId1",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AttachmentId1",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AttachmentId1",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "AttachmentId",
                table: "Companies",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AttachmentId",
                table: "Companies",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Attachments_AttachmentId",
                table: "Companies",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Attachments_AttachmentId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AttachmentId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                table: "Companies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "AttachmentId1",
                table: "Companies",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AttachmentId1",
                table: "Companies",
                column: "AttachmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Attachments_AttachmentId1",
                table: "Companies",
                column: "AttachmentId1",
                principalTable: "Attachments",
                principalColumn: "Id");
        }
    }
}
