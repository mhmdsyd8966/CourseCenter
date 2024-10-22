using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class seedroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "63614cdd-fb33-472e-8c3f-83d1a75b377f", null, "Teacher", "TEACHER" },
                    { "a332353e-b918-4e0a-8446-09135972ed8b", null, "Student", "STUDENT" },
                    { "a8cc6873-26a4-4ab7-9ccf-0db3de4c4471", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63614cdd-fb33-472e-8c3f-83d1a75b377f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a332353e-b918-4e0a-8446-09135972ed8b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8cc6873-26a4-4ab7-9ccf-0db3de4c4471");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Lessons");
        }
    }
}
