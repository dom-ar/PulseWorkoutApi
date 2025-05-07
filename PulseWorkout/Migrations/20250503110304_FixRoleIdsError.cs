using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PulseWorkout.Migrations
{
    /// <inheritdoc />
    public partial class FixRoleIdsError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08a62f3e-c5b7-4cb1-a571-ba7aafc71a2b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "134049db-9132-484e-adf3-a940ab40c1b1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "194389e9-a5a2-4938-bf24-1ce49b316cbd", null, "Member", "MEMBER" },
                    { "a6be59e9-0002-47b8-8123-01b95883ed20", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "194389e9-a5a2-4938-bf24-1ce49b316cbd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6be59e9-0002-47b8-8123-01b95883ed20");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08a62f3e-c5b7-4cb1-a571-ba7aafc71a2b", null, "Member", "MEMBER" },
                    { "134049db-9132-484e-adf3-a940ab40c1b1", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
