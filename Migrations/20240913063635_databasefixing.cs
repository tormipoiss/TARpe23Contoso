using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contoso_University.Migrations
{
    /// <inheritdoc />
    public partial class databasefixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Course",
                newName: "CourseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Course",
                newName: "CourseId");
        }
    }
}
