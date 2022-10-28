using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace userIdentity.Migrations
{
    public partial class fix_the_coursesId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartitem_Courses_coursesId",
                table: "cartitem");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "cartitem");

            migrationBuilder.RenameColumn(
                name: "coursesId",
                table: "cartitem",
                newName: "CoursesId");

            migrationBuilder.RenameIndex(
                name: "IX_cartitem_coursesId",
                table: "cartitem",
                newName: "IX_cartitem_CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_cartitem_Courses_CoursesId",
                table: "cartitem",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartitem_Courses_CoursesId",
                table: "cartitem");

            migrationBuilder.RenameColumn(
                name: "CoursesId",
                table: "cartitem",
                newName: "coursesId");

            migrationBuilder.RenameIndex(
                name: "IX_cartitem_CoursesId",
                table: "cartitem",
                newName: "IX_cartitem_coursesId");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "cartitem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_cartitem_Courses_coursesId",
                table: "cartitem",
                column: "coursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
