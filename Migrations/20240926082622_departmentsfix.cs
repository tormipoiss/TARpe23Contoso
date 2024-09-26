using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contoso_University.Migrations
{
    /// <inheritdoc />
    public partial class departmentsfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Student_StudentAgeID",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_StudentAgeID",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "StudentAgeID",
                table: "Departments",
                newName: "EmployeeAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeAmount",
                table: "Departments",
                newName: "StudentAgeID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_StudentAgeID",
                table: "Departments",
                column: "StudentAgeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Student_StudentAgeID",
                table: "Departments",
                column: "StudentAgeID",
                principalTable: "Student",
                principalColumn: "ID");
        }
    }
}
