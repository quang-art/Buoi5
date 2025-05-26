using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buoi5.Migrations
{
    /// <inheritdoc />
    public partial class Themdulieumau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Thêm dữ liệu vào bảng Grades
            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "gradeId", "gradeName" },
                values: new object[] { 1, "210THA1" }
            );

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "gradeId", "gradeName" },
                values: new object[] { 2, "210THA2" }
            );

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "gradeId", "gradeName" },
                values: new object[] { 3, "210THA3" }
            );

            // Thêm dữ liệu vào bảng Students
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "studentId", "FirstName", "LastName", "gradeId" },
                values: new object[] { 1, "Khuyên", "Bùi", 1 }
            );

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "studentId", "FirstName", "LastName", "gradeId" },
                values: new object[] { 2, "Toàn", "Nguyễn", 1 }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa dữ liệu mẫu khi rollback
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "studentId",
                keyValue: 1
            );

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "studentId",
                keyValue: 2
            );

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "gradeId",
                keyValue: 1
            );

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "gradeId",
                keyValue: 2
            );

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "gradeId",
                keyValue: 3
            );
        
    }
    }
}
