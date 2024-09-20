using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contoso_University.Migrations
{
    /// <inheritdoc />
    public partial class justincase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAssignments_Course_CourseId",
                table: "CourseAssignments");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "CourseAssignments",
                newName: "CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_CourseAssignments_CourseId",
                table: "CourseAssignments",
                newName: "IX_CourseAssignments_CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAssignments_Course_CourseID",
                table: "CourseAssignments",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAssignments_Course_CourseID",
                table: "CourseAssignments");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "CourseAssignments",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseAssignments_CourseID",
                table: "CourseAssignments",
                newName: "IX_CourseAssignments_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAssignments_Course_CourseId",
                table: "CourseAssignments",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
