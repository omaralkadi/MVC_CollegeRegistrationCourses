using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.DAL.Migrations
{
    public partial class UpdateAppUSerCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_appUserCourses",
                table: "appUserCourses");

            migrationBuilder.AddColumn<string>(
                name: "InstructorName",
                table: "appUserCourses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appUserCourses",
                table: "appUserCourses",
                columns: new[] { "CourseId", "UserId", "InstructorName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_appUserCourses",
                table: "appUserCourses");

            migrationBuilder.DropColumn(
                name: "InstructorName",
                table: "appUserCourses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appUserCourses",
                table: "appUserCourses",
                columns: new[] { "CourseId", "UserId" });
        }
    }
}
