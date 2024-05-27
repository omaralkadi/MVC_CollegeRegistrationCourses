using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.DAL.Migrations
{
    public partial class m : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserCourse_AspNetUsers_UserId",
                table: "AppUserCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserCourse_Course_CourseId",
                table: "AppUserCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCourse_Course_CourseId",
                table: "EmployeeCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCourse_Employees_EmployeeId",
                table: "EmployeeCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeCourse",
                table: "EmployeeCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserCourse",
                table: "AppUserCourse");

            migrationBuilder.RenameTable(
                name: "EmployeeCourse",
                newName: "EmployeeCourses");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameTable(
                name: "AppUserCourse",
                newName: "appUserCourses");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeCourse_EmployeeId",
                table: "EmployeeCourses",
                newName: "IX_EmployeeCourses_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserCourse_UserId",
                table: "appUserCourses",
                newName: "IX_appUserCourses_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeCourses",
                table: "EmployeeCourses",
                columns: new[] { "CourseId", "EmployeeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appUserCourses",
                table: "appUserCourses",
                columns: new[] { "CourseId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_appUserCourses_AspNetUsers_UserId",
                table: "appUserCourses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_appUserCourses_Courses_CourseId",
                table: "appUserCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCourses_Courses_CourseId",
                table: "EmployeeCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCourses_Employees_EmployeeId",
                table: "EmployeeCourses",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appUserCourses_AspNetUsers_UserId",
                table: "appUserCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_appUserCourses_Courses_CourseId",
                table: "appUserCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCourses_Courses_CourseId",
                table: "EmployeeCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeCourses_Employees_EmployeeId",
                table: "EmployeeCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeCourses",
                table: "EmployeeCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_appUserCourses",
                table: "appUserCourses");

            migrationBuilder.RenameTable(
                name: "EmployeeCourses",
                newName: "EmployeeCourse");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameTable(
                name: "appUserCourses",
                newName: "AppUserCourse");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeCourses_EmployeeId",
                table: "EmployeeCourse",
                newName: "IX_EmployeeCourse_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_appUserCourses_UserId",
                table: "AppUserCourse",
                newName: "IX_AppUserCourse_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeCourse",
                table: "EmployeeCourse",
                columns: new[] { "CourseId", "EmployeeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserCourse",
                table: "AppUserCourse",
                columns: new[] { "CourseId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserCourse_AspNetUsers_UserId",
                table: "AppUserCourse",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserCourse_Course_CourseId",
                table: "AppUserCourse",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCourse_Course_CourseId",
                table: "EmployeeCourse",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeCourse_Employees_EmployeeId",
                table: "EmployeeCourse",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
