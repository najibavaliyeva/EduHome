using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.Migrations
{
    /// <inheritdoc />
    public partial class relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "courses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "blogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_courses_TeacherId",
                table: "courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_blogs_TeacherId",
                table: "blogs",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_blogs_AspNetUsers_TeacherId",
                table: "blogs",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_courses_AspNetUsers_TeacherId",
                table: "courses",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blogs_AspNetUsers_TeacherId",
                table: "blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_courses_AspNetUsers_TeacherId",
                table: "courses");

            migrationBuilder.DropIndex(
                name: "IX_courses_TeacherId",
                table: "courses");

            migrationBuilder.DropIndex(
                name: "IX_blogs_TeacherId",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "blogs");
        }
    }
}
